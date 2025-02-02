﻿using MstscManager.Forms;
using Newtonsoft.Json.Linq;
using Sunny.UI;

namespace MstscManager.Controls {
    public partial class Add_puty : UIPage {
        public Add_puty() {
            InitializeComponent();
        }

        private void uiRadioButton2_Click(object sender, EventArgs e) {
            Add_server top_form = this.Parent.Parent.Parent.Parent as Add_server;
            top_form.set_port(0);
        }

        private void uiRadioButton1_Click(object sender, EventArgs e) {
            Add_server top_form = this.Parent.Parent.Parent.Parent as Add_server;
            top_form.set_port(22);
        }
        public Dictionary<string, string> get_config() {
            Dictionary<string, string> config = new Dictionary<string, string>();
            if (uiRadioButton1.Checked) config.Add("sec_connect_mode", "ssh");
            if (uiRadioButton2.Checked) config.Add("sec_connect_mode", "telnet");
            return config;
        }
        public void set_config(JObject csobj) {
            uiRadioButton1.Checked = false;
            uiRadioButton1.Checked = csobj["sec_connect_mode"].ToString() == "ssh" ? true : false;
            uiRadioButton2.Checked = csobj["sec_connect_mode"].ToString() == "telnet" ? true : false;
        }
        private void Add_puty_ReceiveParams(object sender, UIPageParamsArgs e) {
            JObject csobj = e.Value as JObject;
            set_config(csobj);
        }
    }

}
