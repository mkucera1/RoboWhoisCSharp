
//WhoIs.cs
//Copyright (C) $2013 Mark Kucera
//This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public License as 
//published by the Free Software Foundation; either version 2 of the License, or (at your option) any later version.
//This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of 
//MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details. 
//You should have received a copy of the GNU General Public License along with this program; if not, write to the Free Software 
//Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA

using System;
using System.Text;
using System.Net;
using System.IO;

namespace RoboWhois
{
    public enum WhoIsType
    {
        Lookup=1,
        Record=2,
        Parts=3,
        Properties=4,
        Availability=5
    }

    public class WhoIs
    {
        private static string _EndPoint = "http://api.robowhois.com/v1/whois/{0}{1}";

        public static string Lookup(string apiKey, string domain, WhoIsType whoIsType)
        {
            string endPoint = _EndPoint.Replace("{0}", domain);

            switch (whoIsType)
            { 
                case WhoIsType.Lookup:
                    endPoint = endPoint.Replace("{1}", "");
                    break;
                case WhoIsType.Record:
                    endPoint = endPoint.Replace("{1}", "/record");
                    break;
                case WhoIsType.Parts:
                    endPoint = endPoint.Replace("{1}", "/parts");
                    break;
                case WhoIsType.Properties:
                    endPoint = endPoint.Replace("{1}", "/properties");
                    break;
                case WhoIsType.Availability:
                    endPoint = endPoint.Replace("{1}", "/availability");
                   break;
            }

            var request = (HttpWebRequest)WebRequest.Create(endPoint);
            request.Method = "GET";

            string authInfo = apiKey + ":X";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            request.Headers.Add("Authorization","Basic " + authInfo);

            var response = (HttpWebResponse)request.GetResponse();

            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}
