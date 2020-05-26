using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProcessOfApplicaion.Models;




namespace ProcessOfApplicaion.Controllers
{
    public class HomeController : Controller
    {
        public object RequestConstants { get; private set; }

       

        public ActionResult Index()
        {
            return View();
        }


        public async Task<ActionResult> Process(string CitizenReg, string CitizenPhone)
        {
            try
            {
                if (CitizenReg != "" && CitizenPhone != "")
                {
                    string url = "https://nd.gv.mn/tet.php?token=o-n_m&a=nd&register=" + CitizenReg.ToString() + "&phone=" + CitizenPhone.ToString();

                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(url);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = await client.GetAsync(url);
                        if (response.IsSuccessStatusCode)
                        {
                            var data = await response.Content.ReadAsStringAsync();
                            var rowInfo = JsonConvert.DeserializeObject<List<ProcessInfo>>(data);

                            if (rowInfo != null && rowInfo.Count > 0)
                            {
                               
                                foreach (var rw in rowInfo)
                                {

                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<div class='row'> <div class='col'>";
                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<div class='mainHeader'>ӨРГӨДЛИЙН ЕРӨНХИЙ МЭДЭЭЛЭЛ / "+ Convert.ToDateTime(rw.mOwn_date).ToString("yyyy-MM-dd") + "/</div> ";
                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<div class='shadow-lg p-3 mb-5 bg-white rounded'>";

                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<table class='table table-borderless table-sm'> ";
                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td><span class='txtBold'>Өргөдлийн төрөл :</span></td>" + "<td>" + rw.mTrans_mode.ToString() + "</td><td><span class='txtBold'>Хэнд хандсан :</span></td>" + "<td>" + rw.mFor_user_name.ToString() + "</td></tr>";
                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td><span class='txtBold'>Өргөдөл гаргагч иргэн :</span></td>" + "<td>" + rw.mSender_name.ToString() + "</td><td><span class='txtBold'>Өргөдөл өгсөн огноо :</span></td>" + "<td>" + Convert.ToDateTime(rw.mOwn_date).ToString("yyyy-MM-dd") + "</td></tr>";
                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td><span class='txtBold'>Утга :</span></td>" + "<td>" + rw.mName.ToString() + "</td><td><span class='txtBold'>Хариу өгөх эцсийн огноо :</span></td>" + "<td>" + Convert.ToDateTime(rw.mAnswer_date).ToString("yyyy-MM-dd") + "</td></tr>";
                                    if (rw.mFor_user_depName != null)
                                    {
                                        ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td><span class='txtBold'>Хэлтэс, газар :</span></td>" + "<td>" + rw.mFor_user_depName.ToString() + "</td><td></td><td></td></tr>";
                                    }
                                    else
                                    {
                                        ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td><span class='txtBold'>Хэлтэс, газар :</span></td><td></td><td></td><td></td></tr>";
                                    }
                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "</table>";

                                    ViewBag.ProcessMainData = ViewBag.ProcessMainData + "</div></div></div>";

                                    // ViewBag.ProcessDataSub = "";

                                    int i = 0;
                                    foreach (var rwSub in rw.shifts)
                                    {
                                        i = i + 1;
                                        if (i > 1)
                                        {
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<div class='row'><div class='col-1'></div> <div class='col-11'>";

                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<div class='subHeader'>ХҮЛЭЭН АВСАН АЖИЛТАН</div>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<div class='shadow-none p-3 mb-5 bg-light rounded'>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<table class='table table-borderless table-sm tableSub'> ";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Хэлтэс :</td>" + "<td>" + rwSub.mReceiver_dep.ToString() + "</td></tr>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Ажилтан :</td>" + "<td>" + rwSub.mReceiver_name.ToString() + "</td></tr>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Албан тушаал :</td>" + "<td>" + rwSub.mReceiver_app.ToString() + "</td></tr>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Ажлын утас :</td>" + "<td>" + rwSub.mReceiver_work_phone.ToString() + "</td></tr>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Гар утас :</td>" + "<td>" + rwSub.mReceiver_phone.ToString() + "</td></tr>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Имэйл хаяг :</td>" + "<td>" + rwSub.mReceiver_mail.ToString() + "</td></tr>";

                                            if (rwSub.mNoteClose.ToString() != "")
                                            {
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td colspan='2'><hr></td></tr>";
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Шийдвэрлэлт :</td>" + "<td>" + rwSub.mNoteClose.ToString() + "</td></tr>";
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Шийдвэрлэсэн огноо :</td>" + "<td>" + Convert.ToDateTime(rwSub.mDateClose).ToString("yyyy-MM-dd") + "</td></tr>";
                                            }
                                            else
                                            {
                                                
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Шийдвэрлэлтийн явц :</td>" + "<td>" + rwSub.mRule.ToString() + "</td></tr>";
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td colspan='2'><hr></td></tr>";
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td colspan='2'>" + rwSub.mOwn_complain.ToString() + "</td></tr>";
                                            }



                                            if (rwSub.mNext_user.ToString() != "0" && rwSub.mNext_user.ToString() != "")
                                            {
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Шилжүүлсэн ажилтан :</td>" + "<td>" + rwSub.mNext_user.ToString() + "</td></tr>";
                                                ViewBag.ProcessMainData = ViewBag.ProcessMainData + "<tr><td>Шилжүүлсэн огноо :</td>" + "<td>" + Convert.ToDateTime(rwSub.mShift_date).ToString("yyyy-MM-dd") + "</td></tr>";
                                            }
                                            
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "</table>";
                                            ViewBag.ProcessMainData = ViewBag.ProcessMainData + "</div></div></div>";



                                        }
                                    }
                                }
                                
                            }
                            else
                            {
                                ViewBag.ProcessMainData = "<p class='text-center'>ӨРГӨДӨЛ БОЛОН ГОМДЛЫН МЭДЭЭЛЭЛ ОЛДСОНГҮЙ.</p>";
                            }


                        }
                        else
                        {
                            ViewBag.ProcessMainData = "<p class='text-center'>ӨРГӨДӨЛ БОЛОН ГОМДЛЫН МЭДЭЭЛЭЛ ОЛДСОНГҮЙ.</p>";
                        }

                    }
                }
                else
                {
                    ViewBag.ProcessMainData = "<p class='text-center'>ӨРГӨДӨЛ БОЛОН ГОМДЛЫН МЭДЭЭЛЭЛ ОЛДСОНГҮЙ.</p>";
                }
            }
            catch (Exception e)
            {
                ViewBag.ProcessMainData = "<p class='text-center'>АЛДАА ГАРЛАА ! Дахин оролдоно уу .</p>";
            }
            return View();
        }



    }
}