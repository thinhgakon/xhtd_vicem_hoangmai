using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using XHTD_WEB.Models;

namespace XHTD_WEB.Business
{
    public class CouponScaleBusiness
    {
        public void CreateCouponScalePdf(CouponScaleModel model) 
        {
            BaseFont f_cb = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/fonts/vuArialBold.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont f_cn = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/fonts/vuArial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            try
            {

                using (System.IO.FileStream fs = new FileStream(HttpContext.Current.Server.MapPath($"~/pdf/coupon_scale/{model.DeliveryCode}_normal.pdf"), FileMode.Create))
                {
                    Document document = new Document(PageSize.A5, 25, 25, 30, 1);
                    PdfWriter writer = PdfWriter.GetInstance(document, fs);
                    document.SetPageSize(iTextSharp.text.PageSize.A5.Rotate());

                    // Add meta information to the document
                    document.AddAuthor("XHTD_HM");
                    document.AddCreator("TrungNC");
                    document.AddKeywords("PDF coupon");
                    document.AddSubject("coupon scale");
                    document.AddTitle("Phiếu cân");

                    // Open the document to enable you to write to the document
                    document.Open();

                    // Makes it possible to add text to a specific place in the document using 
                    // a X & Y placement syntax.
                    PdfContentByte cb = writer.DirectContent;
                    // Add a footer template to the document
                    cb.AddTemplate(PdfFooter(cb), 30, 1);

                    // Add a logo to the invoice
                    iTextSharp.text.Image png = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("~/images/logo/logo.png"));
                    png.ScaleAbsolute(160, 60);
                    png.SetAbsolutePosition(20, 360);
                    cb.AddImage(png);

                    // First we must activate writing
                    cb.BeginText();



                    // First we write out the header information

                    // Start with the invoice type header
                    writeText(cb, "CÔNG TY CỔ PHẦN XI MĂNG VICEM HOÀNG MAI", 250, 400, f_cb, 14);
                    writeText(cb, "THỊ XÃ HOÀNG MAI - NGHỆ AN", 300, 380, f_cb, 14);
                    writeText(cb, "ĐT: 0383.866170 - FAX: 0383.866648", 310, 360, f_cn, 12);
                    if(model.Title.Length > 10)
                    {
                        writeText(cb, $"{model.Title}", 170, 340, f_cb, 14);
                    }
                    else
                    {
                        writeText(cb, $"{model.Title}", 250, 340, f_cb, 14);
                    }
                    writeText(cb, $"Số: {model.CouponNumber} - Ngày: {model.DateCreated}", 220, 320, f_cn, 12);
                    // HEader details; nội dung phiếu
                    //Area1
                    var leftArea1 = 20;
                    var rightArea1 = 140;
                    writeText(cb, "Tên khách hàng:", leftArea1, CalcHeight(5), f_cn, 12); writeText(cb, $"{model.NameOfCustomer.Replace("Kho hàng - ", "")}", rightArea1, CalcHeight(5), f_cn, 12);
                    writeText(cb, "Mã địa bàn:", leftArea1, CalcHeight(6), f_cn, 12); writeText(cb, $"{model.LocationCode}", rightArea1, CalcHeight(6), f_cb, 12);
                    writeText(cb, "Số xe:", leftArea1, CalcHeight(7), f_cn, 12); writeText(cb, $"{model.VehicleCode}", rightArea1, CalcHeight(7), f_cn, 12);
                    writeText(cb, "Loại hàng:", leftArea1, CalcHeight(8), f_cn, 12); writeTextWrap(cb, $"{model.ProductName}", rightArea1, CalcHeight(8), f_cn, 12);
                    writeText(cb, "SL trên đơn hàng:", leftArea1, CalcHeight(10), f_cn, 12); writeText(cb, $"{model.QuantityCoupon}(Tấn)", rightArea1, CalcHeight(10), f_cn, 12);
                    writeText(cb, "Khối lượng cân lần 1:", leftArea1, CalcHeight(11), f_cn, 12); writeText(cb, $"{model.LoadWeightNull}(Tấn)", rightArea1, CalcHeight(11), f_cn, 12);
                    writeText(cb, "Khối lượng cân lần 2:", leftArea1, CalcHeight(12), f_cn, 12); writeText(cb, $"{model.LoadWeightFull}(Tấn)", rightArea1, CalcHeight(12), f_cn, 12);
                    writeText(cb, "Khối lượng hàng:", leftArea1, CalcHeight(13), f_cn, 12); writeText(cb, $"{model.WeightReal}(Tấn)", rightArea1, CalcHeight(13), f_cn, 12);
                    writeText(cb, "Số Seri niêm phong:    ", leftArea1, CalcHeight(14), f_cn, 12); writeText(cb, $"{model.Seri}", rightArea1, CalcHeight(14), f_cn, 12);


                    //Area2
                    var leftArea2 = 350;
                    var rightArea2 = 470;
                    writeText(cb, "Số Rơ moóc:", leftArea2, CalcHeight(5), f_cn, 12); writeText(cb, $"{model.Mooc}", rightArea2, CalcHeight(5), f_cn, 12);
                    writeText(cb, "Số lô:", leftArea2, CalcHeight(6), f_cn, 12); writeText(cb, $"{model.Package}", rightArea2, CalcHeight(6), f_cb, 12);
                    writeText(cb, "Số đơn hàng:", leftArea2, CalcHeight(7), f_cn, 12); writeText(cb, $"{model.DeliveryCode}", rightArea2, CalcHeight(7), f_cn, 12);
                    writeText(cb, "Ngày giờ cân lần 1:", leftArea2, CalcHeight(8), f_cn, 12); writeText(cb, $"{model.TimeScaleIn}", rightArea2, CalcHeight(8), f_cn, 12);
                    writeText(cb, "Ngày giờ cân lần 2:", leftArea2, CalcHeight(9), f_cn, 12); writeText(cb, $"{model.TimeScaleOut}", rightArea2, CalcHeight(9), f_cn, 12);
                    writeText(cb, "Số lượng niêm phong:", leftArea2, CalcHeight(10), f_cn, 12); writeText(cb, $"{model.CountSeal} (Cái)", rightArea2, CalcHeight(10), f_cn, 12);

                    //Area 3

                    //writeText(cb, "Người cân", 40, CalcHeight(16), f_cb, 14);
                    //writeText(cb, "Người niêm phong", 150, CalcHeight(16), f_cb, 14);
                    //writeText(cb, "Khách hàng", 330, CalcHeight(16), f_cb, 14);
                    //writeText(cb, "Bảo vệ", 500, CalcHeight(16), f_cb, 14);
                    // ABC
                     writeText(cb, $"Tích hợp với PXK điện tử số {model.DocNum}", 180, CalcHeight(18), f_cb, 14);

                    // NOTE! You need to call the EndText() method before we can write graphics to the document!
                    cb.EndText();
                    // Separate the header from the rows with a line
                    // Draw a line by setting the line width and position
                    cb.SetLineWidth(0f);
                    cb.MoveTo(40, 570);
                    cb.LineTo(560, 570);
                    cb.Stroke();


                    // Close the document, the writer and the filestream!
                    document.Close();
                    writer.Close();
                    fs.Close();

                    RotatePages(HttpContext.Current.Server.MapPath($"~/pdf/coupon_scale/{model.DeliveryCode}_normal.pdf"), HttpContext.Current.Server.MapPath($"~/pdf/coupon_scale/{model.DeliveryCode}.pdf"), 90);
                }
            }
            catch (Exception rror)
            {
            }
        }
        private int CalcHeight(int lamp)
        {
            return 400 - lamp * 20;
        }
        private void RotatePages(string pdfFilePath, string outputPath, int rotateDegree)
        {
            try
            {
                PdfReader reader = new PdfReader(pdfFilePath);
                int pagesCount = reader.NumberOfPages;

                for (int n = 1; n <= pagesCount; n++)
                {
                    PdfDictionary page = reader.GetPageN(n);
                    PdfNumber rotate = page.GetAsNumber(PdfName.ROTATE);
                    int rotation =
                            rotate == null ? rotateDegree : (rotate.IntValue + rotateDegree) % 360;

                    page.Put(PdfName.ROTATE, new PdfNumber(rotation));
                }

                PdfStamper stamper = new PdfStamper(reader, new FileStream(outputPath, FileMode.Create));
                stamper.Close();
                reader.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void writeText(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            cb.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Text, X, Y, 0);
        }
        public void writeTextWrap(PdfContentByte cb, string Text, int X, int Y, BaseFont font, int Size)
        {
            cb.SetFontAndSize(font, Size);
            if (Text.Length > 30)
            {
                var s1 = Text.Substring(0, 30);
                var s2 = Text.Replace(s1, "");
                cb.ShowTextAligned(Element.ALIGN_MIDDLE, s1, X, Y, 0);
                cb.ShowTextAligned(Element.ALIGN_MIDDLE, s2, X, Y - 20, 0);
            }
            else
            {
                cb.SetFontAndSize(font, Size);
                cb.ShowTextAligned(Element.ALIGN_MIDDLE, Text, X, Y, 0);
            }

        }
        public PdfTemplate PdfFooter(PdfContentByte cb)
        {
            BaseFont f_cb = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/fonts/vuArialBold.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            BaseFont f_cn = BaseFont.CreateFont(HttpContext.Current.Server.MapPath("~/fonts/vuArial.ttf"), BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            PdfTemplate tmpFooter = cb.CreateTemplate(580, 30);
            tmpFooter.MoveTo(1, 1);
            tmpFooter.SetColorFill(BaseColor.BLACK);
            tmpFooter.Stroke();
            tmpFooter.BeginText();
            tmpFooter.SetFontAndSize(f_cn, 9);
            var footerText = $@"Đ/c: Phường Quỳnh Thiện, Thị xã Hoàng Mai, Tỉnh Nghệ An - Tel: 02383 866 170 - Fax: 02383 866 648 - Web: www.ximanghoangmai.com.vn";
            tmpFooter.ShowTextAligned(PdfContentByte.ALIGN_LEFT, footerText, 0, 15, 0);
            tmpFooter.EndText();
            cb.SetLineWidth(0f);
            cb.MoveTo(30, 30);
            cb.LineTo(580, 30);
            cb.Stroke();
            return tmpFooter;
        }
    }
}