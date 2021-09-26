using Project_Jayesh_SWF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Project_Jayesh_SWF.Controllers
{
    public class ValuesController : ApiController
    {
        EmployeeEntities employeeEntities = new EmployeeEntities();

        [HttpGet, HttpPost]
        [Route("api/general/AddorUpdateEmployeeDetails")]
        public IHttpActionResult AddorUpdateEmployeeDetails()
        {
            try
            {
                string employeeID = HttpContext.Current.Request.Params["employeeID"];
                string name = HttpContext.Current.Request.Params["name"];
                string age = HttpContext.Current.Request.Params["age"];
                string gender = HttpContext.Current.Request.Params["gender"];
                string address = HttpContext.Current.Request.Params["address"];
                string city = HttpContext.Current.Request.Params["city"];
                string state = HttpContext.Current.Request.Params["state"];
                string country = HttpContext.Current.Request.Params["country"];
                string totalExp = HttpContext.Current.Request.Params["totalExp"];
                string prevCompany = HttpContext.Current.Request.Params["prevCompany"];
                string designation = HttpContext.Current.Request.Params["designation"];

                if (employeeID != "")
                {
                    string sSql = "Update EmployeeMaster set Name='" + name + "', Age=" + age + ", Gender='" + gender + "'," +
                                    "Address='" + address + "', City='" + city + "', State='" + state + "', Country='" + country + "', " +
                                    "Experience='" + totalExp + "', PreviousCompany='" + prevCompany + "', " +
                                    "Designation='" + designation + "' where ID=" + employeeID + "";
                    int count = 0;
                    count = employeeEntities.Database.ExecuteSqlCommand(sSql);
                    if (count > 0)
                        return Json((object)new { success = true, msg = "Employee Details Updated Successfully!!" });
                    else
                        return Json((object)new { success = false, msg = "Employee Details Not Updated!!Some Error Occur" });
                }
                else
                {
                    int count = 0;
                    string sSql = "select count(*) from EmployeeMaster where Name='" + name + "' and Age=" + age + " and " +
                                "Gender='" + gender + "' and Address='" + address + "' and City='" + city + "' and State='" + state + "' " +
                                "and Country='" + country + "'";
                    count = employeeEntities.Database.SqlQuery<int>(sSql).FirstOrDefault();
                    if (count == 0)
                    {
                        sSql = "insert into EmployeeMaster(Name, Age, Gender, Address, City, State, Country, " +
                                      "Experience, PreviousCompany, Designation) values('" + name + "'," + age + "," +
                                      "'" + gender + "','" + address + "','" + city + "','" + state + "','" + country + "'," +
                                      "'" + totalExp + "','" + prevCompany + "','" + designation + "')";

                        count = employeeEntities.Database.ExecuteSqlCommand(sSql);
                        if (count > 0)
                            return Json((object)new { success = true, msg = "Employee Details Added Successfully!!" });
                        else
                            return Json((object)new { success = false, msg = "Employee Details Not Added!!Some Error Occur" });
                    }
                    else
                        return Json((object)new { success = false, msg = "Employee Details Already Exists!!" });
                }
            }
            catch (Exception ex)
            {
                return Json((object)new { success = false, msg = ex.Message });
            }
        }

        [HttpGet, HttpPost]
        [Route("api/general/GetEmployeeDetails")]
        public IHttpActionResult GetEmployeeDetails()
        {
            try
            {
                List<EmployeeMaster> tableList = new List<EmployeeMaster>();
                string sSql = "SELECT * FROM EmployeeMaster";
                tableList = employeeEntities.Database.SqlQuery<EmployeeMaster>(sSql).ToList();
                if (tableList.Count > 0)
                {
                    tableList = tableList.OrderByDescending(x => x.ID).ToList();
                    return Json((object)new { success = true, tableList });
                }
                else
                    return Json((object)new { success = false, msg = "Data Not Found!!" });

            }
            catch (Exception ex)
            {
                return Json((object)new { success = false, msg = ex.Message });
            }
        }

        [HttpGet, HttpPost]
        [Route("api/general/GetEmployeeDetailsByID")]
        public IHttpActionResult GetEmployeeDetailsByID(int id)
        {
            try
            {
                EmployeeMaster employeeMaster = new EmployeeMaster();
                string sSql = "SELECT * FROM EmployeeMaster where ID=" + id + "";
                employeeMaster = employeeEntities.Database.SqlQuery<EmployeeMaster>(sSql).FirstOrDefault();
                if (employeeMaster != null)
                    return Json((object)new { success = true, employeeMaster });
                else
                    return Json((object)new { success = false, msg = "Data Not Found!!" });

            }
            catch (Exception ex)
            {
                return Json((object)new { success = false, msg = ex.Message });
            }
        }

        [HttpGet, HttpPost]
        [Route("api/general/DeleteEmployeeDetailsByID")]
        public IHttpActionResult DeleteEmployeeDetailsByID(int id)
        {
            int count = 0;
            string sSql = "Delete from EmployeeMaster where ID=" + id + "";
            count = employeeEntities.Database.ExecuteSqlCommand(sSql);
            if (count > 0)
                return Json((object)new { success = true, msg = "Employee Details Deleted Successfully!!" });
            else
                return Json((object)new { success = false, msg = "Employee Details Not Deleted!!Some Error Occur" });
        }
    }
}
