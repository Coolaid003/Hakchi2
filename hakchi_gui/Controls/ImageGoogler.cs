﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace com.clusterrr.hakchi_gui.Controls
{
    public partial class ImageGoogler : UserControl, IDisposable
    {
        public struct SearchQuery
        {
            public string Query;
            public string AdditionalVariables;
        }
        public delegate void ImageReceived(Image image);
        public delegate void ImageDeselected();
        public event ImageReceived OnImageSelected;
        public event ImageReceived OnImageDoubleClicked;
        public event ImageDeselected OnImageDeselected;
        public List<SearchQuery> Queries { get; } = new List<SearchQuery>();
        private Thread searchThread;
        private List<string> downloadedUrls = new List<string>();

        public void Deselect()
        {
            
            foreach (var item in listView.Items.Cast<ListViewItem>())
            {
                if (item.Selected)
                    item.Selected = false;
            }
        }
        public void RunQuery(params Bitmap[] customResults)
        {
            imageList.Images.Clear();
            listView.Items.Clear();
            downloadedUrls.Clear();

            if (searchThread != null)
            {
                if (searchThread.IsAlive)
                {
                    #warning Refactor this to get rid of Thread.Abort!
                    searchThread.Abort();
                    searchThread = null;
                }
            }
            searchThread = new Thread(() =>
            {
                foreach (var image in customResults)
                    ShowImage(image);

                foreach (var query in Queries)
                    SearchThread(query.Query, query.AdditionalVariables);
            });
            searchThread.Start();
        }

        public static string[] GetImageUrls(string query, string additionalVariables = "", int tryCount = 0)
        {
            try
            {
                if (tryCount > 0)
                {
                    Trace.WriteLine($"Retry #{tryCount}");
                }

                var url = string.Format("https://www.google.com/search?q={0}&source=lnms&tbm=isch{1}", HttpUtility.UrlEncode(query), additionalVariables.Length > 0 ? $"&{additionalVariables}" : "");
                Trace.WriteLine("Web request: " + url);
                var request = WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;
                (request as HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:98.0) Gecko/20100101 Firefox/98.0";
                request.Timeout = 10000;
                var response = request.GetResponse();
                Stream dataStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                reader.Close();
                response.Close();
                //Trace.WriteLine("Web response: " + responseFromServer);

                var urls = new List<string>();
                string search = @"\""ou\""\:\""(?<url>.+?)\""";
                MatchCollection matches = Regex.Matches(responseFromServer, search);
                foreach (Match match in matches)
                {
                    urls.Add(HttpUtility.UrlDecode(match.Groups[1].Value.Replace("\\u00", "%")));
                }

                // For some reason Google returns different data for different users (IPs?)
                // This is an alternative method
                search = @"imgurl=(.*?)&";
                matches = Regex.Matches(responseFromServer, search);
                foreach (Match match in matches)
                {
                    // Not sure about it.
                    urls.Add(HttpUtility.UrlDecode(match.Groups[1].Value.Replace("\\u00", "%")));
                }

                // For some reason Google returns different data for different users (IPs?)
                // This is alternative method #2
                search = "\\]\\n?,\\[\"([^\"]+)\",\\d+,\\d+]";
                matches = Regex.Matches(responseFromServer, search);
                foreach (Match match in matches)
                {
                    // Not sure about it.
                    if (Uri.IsWellFormedUriString(match.Groups[1].Value, UriKind.Absolute))
                    {
                        urls.Add(HttpUtility.UrlDecode(match.Groups[1].Value.Replace("\\u00", "%")));
                    }
                }

                if (urls.Count == 0)
                {
                    Trace.WriteLine("No results found");
                }

                return urls.ToArray();
            }
            catch(Exception ex)
            {
                if (tryCount < 5)
                {
                    Trace.WriteLine(ex?.Message);
                    Trace.WriteLine(ex?.InnerException?.Message);
                    return GetImageUrls(query, additionalVariables, tryCount + 1);
                }
                else
                {
                    Trace.WriteLine(ex?.Message);
                    Trace.WriteLine(ex?.InnerException?.Message);
                    return new string[] { };
                }
            }
            
        }

        private void SearchThread(string query, string additionalVariables)
        {
            try
            {
                var urls = GetImageUrls(query, additionalVariables);
                foreach (var url in urls)
                {
                    try
                    {
                        if (!downloadedUrls.Contains(url))
                        {
                            downloadedUrls.Add(url);
                            Trace.WriteLine("Downloading image: " + url);
                            var image = DownloadImage(url);
                            ShowImage(image);
                        }
                    }
                    catch { }
                }
            }
            catch (ThreadAbortException) { }
        }

        protected void ShowImage(Image image)
        {
            try
            {
                if (this.Disposing) return;
                if (InvokeRequired)
                {
                    Invoke(new Action<Image>(ShowImage), new object[] { image });
                    return;
                }

                
                int i = imageList.Images.Count;
                const int side = 256;
                var imageRect = new Bitmap(side, side, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                var gr = Graphics.FromImage(imageRect);
                gr.Clear(Color.White);
                if (image.Height > image.Width)
                    gr.DrawImage(image, new Rectangle((side - side * image.Width / image.Height) / 2, 0, side * image.Width / image.Height, side),
                        new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                else
                    gr.DrawImage(image, new Rectangle(0, (side - side * image.Height / image.Width) / 2, side, side * image.Height / image.Width),
                        new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
                gr.Flush();
                listView.BeginUpdate();
                imageList.Images.Add(imageRect);
                var item = new ListViewItem(image.Width + "x" + image.Height);
                item.ImageIndex = i;
                item.Tag = image;
                listView.Items.Add(item);
                listView.EndUpdate();
                listView.Update();
            }
            catch { }
        }

        public static Image DownloadImage(string url)
        {
            var request = HttpWebRequest.Create(url);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 5000;
            ((HttpWebRequest)request).UserAgent =
                "Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.8.1.4) Gecko/20070515 Firefox/2.0.0.4";
            ((HttpWebRequest)request).KeepAlive = false;
            var response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            var image = Image.FromStream(dataStream);
            dataStream.Dispose();
            response.Close();
            return image;
        }
        public ImageGoogler()
        {
            InitializeComponent();
        }

        private Image GetSelectedImage()
        {
            if (listView.SelectedItems.Count == 0) return null;
            return listView.SelectedItems[0].Tag as Image;
        }

        private void listView_DoubleClick(object sender, EventArgs e)
        {
            Image selected;
            if ((selected = GetSelectedImage()) != null)
                this.OnImageDoubleClicked?.Invoke(selected);
        }

        private void listView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Image selected;
            if ((selected = GetSelectedImage()) != null)
            {
                this.OnImageSelected?.Invoke(selected);
            }
            else
            {
                this.OnImageDeselected?.Invoke();
            }
        }
    }
}
