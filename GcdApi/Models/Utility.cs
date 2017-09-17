using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GcdApi.Models
{
    public class Utility
    {
#if (DEBUG)
        private string con = @"Server=TODD\SQLEXPRESS;Database=GCD;Integrated Security=True;";
#else
        private string con = Properties.Settings.Default.Connection;
#endif

        private Func<IDataRecord, ServiceDto> ServiceRowToDto => row => new ServiceDto
        {
            id = (int)row[nameof(ServiceDto.id)],
            approved = row[nameof(ServiceDto.approved)].ToString(),
            background = row[nameof(ServiceDto.background)].ToString(),
            categories = row[nameof(ServiceDto.categories)].ToString(),
            field_access = row[nameof(ServiceDto.field_access)].ToString(),
            field_access2 = row[nameof(ServiceDto.field_access2)].ToString(),
            field_accessinfo = row[nameof(ServiceDto.field_accessinfo)].ToString(),
            field_address2 = row[nameof(ServiceDto.field_address2)].ToString(),
            field_address3 = row[nameof(ServiceDto.field_address3)].ToString(),
            field_addtnl_address = row[nameof(ServiceDto.field_addtnl_address)].ToString(),
            field_admincom = row[nameof(ServiceDto.field_admincom)].ToString(),
            field_area = row[nameof(ServiceDto.field_area)].ToString(),
            field_charityno = row[nameof(ServiceDto.field_charityno)].ToString(),
            field_city = row[nameof(ServiceDto.field_city)].ToString(),
            field_cost = row[nameof(ServiceDto.field_cost)].ToString(),
            field_county = row[nameof(ServiceDto.field_county)].ToString(),
            field_description = row[nameof(ServiceDto.field_description)].ToString(),
            field_email = row[nameof(ServiceDto.field_email)].ToString(),
            field_email2 = row[nameof(ServiceDto.field_email2)].ToString(),
            field_exporttag = row[nameof(ServiceDto.field_exporttag)].ToString(),
            field_fblink = row[nameof(ServiceDto.field_fblink)].ToString(),
            field_ind_contact = row[nameof(ServiceDto.field_ind_contact)].ToString(),
            field_inforec = row[nameof(ServiceDto.field_inforec)].ToString(),
            field_ipcode = row[nameof(ServiceDto.field_ipcode)].ToString(),
            field_latitude = row[nameof(ServiceDto.field_latitude)].ToString(),
            field_longitude = row[nameof(ServiceDto.field_longitude)].ToString(),
            field_opening = row[nameof(ServiceDto.field_opening)].ToString(),
            field_phone = row[nameof(ServiceDto.field_phone)].ToString(),
            field_phone2 = row[nameof(ServiceDto.field_phone2)].ToString(),
            field_phoneinfo = row[nameof(ServiceDto.field_phoneinfo)].ToString(),
            field_postal = row[nameof(ServiceDto.field_postal)].ToString(),
            field_postcode = row[nameof(ServiceDto.field_postcode)].ToString(),
            field_prev = row[nameof(ServiceDto.field_prev)].ToString(),
            field_recno = row[nameof(ServiceDto.field_recno)].ToString(),
            field_referral = row[nameof(ServiceDto.field_referral)].ToString(),
            field_related = row[nameof(ServiceDto.field_related)].ToString(),
            field_scotcharno = row[nameof(ServiceDto.field_scotcharno)].ToString(),
            field_services = row[nameof(ServiceDto.field_services)].ToString(),
            field_shirearea = row[nameof(ServiceDto.field_shirearea)].ToString(),
            field_street = row[nameof(ServiceDto.field_street)].ToString(),
            field_transport = row[nameof(ServiceDto.field_transport)].ToString(),
            field_twitter = row[nameof(ServiceDto.field_twitter)].ToString(),
            field_website = row[nameof(ServiceDto.field_website)].ToString(),
            hits = row[nameof(ServiceDto.hits)].ToString(),
            icon = row[nameof(ServiceDto.icon)].ToString(),
            image = row[nameof(ServiceDto.image)].ToString(),
            ip = row[nameof(ServiceDto.ip)].ToString(),
            last_update = row[nameof(ServiceDto.last_update)].ToString(),
            metadesc = row[nameof(ServiceDto.metadesc)].ToString(),
            metakey = row[nameof(ServiceDto.metakey)].ToString(),
            owner = row[nameof(ServiceDto.owner)].ToString(),
            Params = row["params"].ToString(),
            published = row[nameof(ServiceDto.published)].ToString(),
            publish_down = row[nameof(ServiceDto.publish_down)].ToString(),
            publish_up = row[nameof(ServiceDto.publish_up)].ToString(),
            title = row[nameof(ServiceDto.title)].ToString(),
            updating_ip = row[nameof(ServiceDto.updating_ip)].ToString(),
            updating_user = row[nameof(ServiceDto.updating_user)].ToString(),
        };

        public GcdData GetServies(int pageNumber)
        {
            List<ServiceDto> services = new List<ServiceDto>();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = $"SELECT * FROM Services ORDER BY id OFFSET {(pageNumber - 1) * 20} ROWS FETCH NEXT 20 ROWS ONLY;";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    services = oReader.ReadList(ServiceRowToDto);

                    myConnection.Close();
                }
            }

            return new GcdData
            {
                PageNumber = pageNumber,
                Services = services.ToArray()
            };
        }

        public GcdData GetServies(string query, int pageNumber)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return GetServies(1);
            }

            query = query.Trim();

            List<ServiceDto> services = new List<ServiceDto>();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = $"SELECT * FROM Services WHERE {nameof(ServiceDto.metakey)} LIKE '%{query}%' ORDER BY id OFFSET {(pageNumber - 1) * 20} ROWS FETCH NEXT 20 ROWS ONLY;";
                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    services = oReader.ReadList(ServiceRowToDto);

                    myConnection.Close();
                }
            }

            return new GcdData
            {
                PageNumber = pageNumber,
                Services = services.ToArray()
            };
        }

        public GcdData GetServiesByLocation(decimal latitude, decimal longitude, decimal maxDistance)
        {
            List<ServiceDto> services = new List<ServiceDto>();

            using (SqlConnection myConnection = new SqlConnection(con))
            {
                string oString = $@"SELECT TOP 10 * 
        FROM(SELECT *, (((acos(sin(({latitude} * pi() / 180)) *
        sin((field_latitude * pi() / 180)) + cos(({latitude} * pi() / 180)) *
        cos((field_latitude * pi() / 180)) * cos((({longitude} -
        field_longitude) * pi() / 180)))) * 180 / pi()) * 60 * 1.1515 * 1.609344)
        as distance
        FROM Services)[Services]
        WHERE distance <= {maxDistance}
        ORDER BY distance";

                SqlCommand oCmd = new SqlCommand(oString, myConnection);
                myConnection.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    services = oReader.ReadList(ServiceRowToDto);

                    myConnection.Close();
                }
            }

            return new GcdData
            {
                Services = services.ToArray()
            };
        }
    }

    public static class Extenions
    {
        public static List<T> ReadList<T>(this IDataReader reader,
                                  Func<IDataRecord, T> generator)
        {
            var list = new List<T>();
            while (reader.Read())
                list.Add(generator(reader));
            return list;
        }
    }
}