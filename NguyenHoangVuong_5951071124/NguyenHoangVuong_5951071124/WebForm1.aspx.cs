using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NguyenHoangVuong_5951071124
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var req = WebRequest.Create("https://graph.facebook.com/utc2hcmc/posts?access_token=EAAAAZAw4FxQIBAPUAyXltoLxToviNa7fs6UB8wtp9ZAvFPvA5m0d8kxVTmXWXTXcwV4WRZAADyTLG5x2UZBrFgwKEWkPGZCC08504kFa86lGXZBGQLmbxjsYWa7Xzf1ZAZBxmbGIItbcK7RZBEAsEs4MHOW58kHCZA9RnvhlZCktp0Rq1uoEUkZAhY8V9PPKmVek6OcZD" + "");
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream stream = res.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string responseString = reader.ReadToEnd();
            dynamic data = JsonConvert.DeserializeObject(responseString);

            var result = new List<resource>();

            foreach(var i in data.data){
                result.Add(new resource
                {
                    Time = i.created_time,
                    Message =i.message,
                    Link = i.actions[0].link
                });
            }
            
            string s = "<h2>3 bài đăng gần nhất</h2></br>";
            for(int i = 0; i < 3; i++)
            {
                s += "<b></br>Bài thứ</b> " + (i + 1) + " : </br></br>";
                s += "<b>Ngày đăng : </b>" + result[i].Time + "</br>";
                s += "<b>Nội dung : </b>" + result[i].Message + "</br>";
                s += "<b>Link bài : </b>" + result[i].Link + "</br></br>";
                s += "---------------------------------------------------------";
            }

            Label1.Text = s;
        }
    }
}