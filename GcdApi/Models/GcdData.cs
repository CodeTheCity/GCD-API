using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GcdApi.Models
{
    public class GcdData
    {
        public ServiceDto[] Services { get; set; }
    }

    public class ServiceDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string hits { get; set; }
        public string published { get; set; }
        public string approved { get; set; }
        public string publish_up { get; set; }
        public string publish_down { get; set; }
        public string owner { get; set; }
        public string icon { get; set; }
        public string image { get; set; }
        public string background { get; set; }
        public string Params { get; set; }
        public string ip { get; set; }
        public string last_update { get; set; }
        public string updating_user { get; set; }
        public string updating_ip { get; set; }
        public string metakey { get; set; }
        public string metadesc { get; set; }
        public string categories { get; set; }
        public string field_prev { get; set; }
        public string field_recno { get; set; }
        public string field_description { get; set; }
        public string field_street { get; set; }
        public string field_address2 { get; set; }
        public string field_address3 { get; set; }
        public string field_city { get; set; }
        public string field_county { get; set; }
        public string field_postcode { get; set; }
        public string field_phone { get; set; }
        public string field_phoneinfo { get; set; }
        public string field_longitude { get; set; }
        public string field_latitude { get; set; }
        public string field_phone2 { get; set; }
        public string field_email { get; set; }
        public string field_website { get; set; }
        public string field_ind_contact { get; set; }
        public string field_charityno { get; set; }
        public string field_scotcharno { get; set; }
        public string field_services { get; set; }
        public string field_area { get; set; }
        public string field_access { get; set; }
        public string field_accessinfo { get; set; }
        public string field_access2 { get; set; }
        public string field_addtnl_address { get; set; }
        public string field_opening { get; set; }
        public string field_cost { get; set; }
        public string field_referral { get; set; }
        public string field_transport { get; set; }
        public string field_related { get; set; }
        public string field_inforec { get; set; }
        public string field_postal { get; set; }
        public string field_admincom { get; set; }
        public string field_ipcode { get; set; }
        public string field_shirearea { get; set; }
        public string field_fblink { get; set; }
        public string field_exporttag { get; set; }
        public string field_twitter { get; set; }
        public string field_email2 { get; set; }
    }
}