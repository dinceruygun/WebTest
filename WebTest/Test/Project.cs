using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebSiteTest.Test
{
    public class Project
    {
        private string _url;

        public string Url
        {
            get { return _url; }

            set
            {
                _url = value;

                var host = new StringBuilder();
                if (_url.ToLower().IndexOf("http://", StringComparison.Ordinal) < 0) host.Append("http://");
                host.Append(_url);
                if (_url[_url.Length - 1] != '/') host.Append("/");

                _url = host.ToString();
            }
        }

    }
}
