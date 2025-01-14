﻿using Microsoft.Win32;
using Sunny.UI;
using MstscManager.Utils;

namespace MstscManager.Controls {
    public partial class Change_pass : UIPage {
        public Change_pass() {
            InitializeComponent();
        }

        private void uiButton1_Click(object sender, EventArgs e) {
            RegistryKey rkey = Registry.CurrentUser;
            RegistryKey key = rkey.CreateSubKey(@"SOFTWARE\MstscManager");
            key = rkey.OpenSubKey(@"SOFTWARE\MstscManager",true);
            string password  = key.GetValue("mstsc_pass","").ToString();
            if (password == "") { ShowErrorTip("密码为空不能修改"); key.Close(); rkey.Close(); return; }
            string old_pass = uiTextBox1.Text;
            string new_pass = uiTextBox2.Text;
            string new_pass2 = uiTextBox3.Text;
            if(new_pass2 != new_pass) { ShowErrorTip("两次输入的新密码不一致！"); key.Close(); rkey.Close(); return; }
            string new_pass_e = common_tools.md5(new_pass);
            if (password == new_pass_e) { ShowErrorTip("新旧密码不能相同！"); key.Close(); rkey.Close(); return; }
            if (password != common_tools.md5(old_pass)) { ShowErrorTip("旧密码错误！"); key.Close(); rkey.Close(); return; }
            key.SetValue("mstsc_pass", new_pass_e);
            DbSqlHelper.UpdatePassword(new_pass_e.Substring(0,16));
            key.Close(); rkey.Close();
            ShowSuccessTip("密码修改成功！");
            Thread.Sleep(1000);
            ShowSuccessTip("为了软件了稳定，请重新启动！");
            Thread.Sleep(1000);
            System.Environment.Exit(0);
        }
    }
}
