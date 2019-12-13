using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CurrencyRates
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private String RosselBank()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            String Response = wc.DownloadString("http://www.rshb.ru/branches/saratov/");
            String Rate = System.Text.RegularExpressions.Regex.Match(Response, @"<td>([0-9]+\.[0-9]+)</td>").Groups[1].Value;
            return "Россельхозбанк: " + Rate + " р. \r\n";
        }

        private String Mfd()
        {
            System.Net.WebClient wc = new System.Net.WebClient();
            String Response = wc.DownloadString("http://mfd.ru/currency/?currency=USD");
            String Rate = System.Text.RegularExpressions.Regex.Match(Response, @"<td>([0-9]+\.[0-9]+)</td>").Groups[1].Value;
            return "Mfd.ru: " + Rate + " р. \r\n";

        }

        private String YandexCurrency()
        {

            System.Net.WebClient wc = new System.Net.WebClient();
            String Response = wc.DownloadString("https://yandex.ru/news/quotes/1.html");
            String Rate = System.Text.RegularExpressions.Regex.Match(Response, @"<td class=""quote__value""><span class=""quote__sgn""></span>([0-9]+\,[0-9]+)</td>").Groups[1].Value;
            return "Яндекс: " + Rate + " р. \r\n";
        }
        
        private String FinanceRambler()
        {
            //<div class="finance-currency-plate__currency">63.7542</ div >

            System.Net.WebClient wc = new System.Net.WebClient();
            String Response = wc.DownloadString("https://finance.rambler.ru/currencies/USD/");
            String Rate = System.Text.RegularExpressions.Regex.Match(Response, @"<div class=""finance-exchange-rate__value"">\n([0-9]+\.[0-9]+\n)</div>").Groups[1].Value;
            return "Рамблер/Финансы: " + Rate + " р. \r\n";
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Text = RosselBank() + Mfd() + YandexCurrency() + FinanceRambler();
        }

    }
}
