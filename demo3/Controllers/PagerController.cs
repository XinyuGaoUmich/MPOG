using demo3.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace demo3.Controllers
{
    public class PagerController : Controller
    {
        // GET: Pager
        private MPOG_XinyuEntities4 db2 = new MPOG_XinyuEntities4();
        public ActionResult Index(int? id)
        {

            if (Session["roles"] != null && Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("Editable");
            }
            return View(db2.Pager(id).First());
        }

        public ActionResult Editable(int? id)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("Index");
            }
            return View(db2.Pager(id).ToList());
        }

        [HttpPost]
        [ValidateInput(false)]
        public FileResult Export(string GridHtml)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                StringReader sr = new StringReader(GridHtml);
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                XMLWorkerHelper.GetInstance().ParseXHtml(writer, pdfDoc, sr);
                pdfDoc.Close();
                return File(stream.ToArray(), "application/pdf", "Grid.pdf");
            }
        }
     
    // GET: Pager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pager/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pager/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pager/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
