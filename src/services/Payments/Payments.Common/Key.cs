using Stripe;
using System;
using System.Configuration;

namespace Payments.Common
{
    public static class Key
    {
        #region Secret Key
        public static void SecretKey()
        {
            // Set your secret key. Remember to switch to your live secret key in production!
            // See your keys here: https://dashboard.stripe.com/account/apikeys
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["Secret_Key_Strinpe"];
        }
        #endregion
    }
}
