using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTrackingSystem.Classes
{
    public class Ean13
    {
        private string _sName = "EAN13";

        private float _fMinimumAllowableScale = 0.8f;
        private float _fMaximumAllowableScale = 2.0f;

        private float _fWidth = 30.29f;
        private float _fHeight = 5.93f;
        private float _fFontSize = 8.0f;
        private float _fScale = 1.0f;

        private string[] _aOddLeft = { "0001101", "0011001", "0010011", "0111101",
                                          "0100011", "0110001", "0101111", "0111011",
                                          "0110111", "0001011" };

        private string[] _aEvenLeft = { "0100111", "0110011", "0011011", "0100001",
                                           "0011101", "0111001", "0000101", "0010001",
                                           "0001001", "0010111" };

        private string[] _aRight = { "1110010", "1100110", "1101100", "1000010",
                                        "1011100", "1001110", "1010000", "1000100",
                                        "1001000", "1110100" };

        private string _sQuiteZone = "000000000";

        private string _sLeadTail = "101";

        private string _sSeparator = "01010";

        private string _sCountryCode = "0";
        private string _sManufacturerCode;
        private string _sProductCode;
        private string _sChecksumDigit;

        public Ean13()
        {

        }

        public Ean13(string mfgNumber, string productId)
        {
            this.CountryCode = "1";
            this.ManufacturerCode = mfgNumber;
            this.ProductCode = productId;
            this.CalculateChecksumDigit();
        }

        public Ean13(string countryCode, string mfgNumber, string productId)
        {
            this.CountryCode = countryCode;
            this.ManufacturerCode = mfgNumber;
            this.ProductCode = productId;
            this.CalculateChecksumDigit();
        }

        public Ean13(string countryCode, string mfgNumber, string productId, string checkDigit)
        {
            this.CountryCode = countryCode;
            this.ManufacturerCode = mfgNumber;
            this.ProductCode = productId;
            this.ChecksumDigit = checkDigit;
        }

        public void DrawEan13Barcode(System.Drawing.Graphics g, System.Drawing.PointF pt)
        {
            float width = this.Width * this.Scale;
            float height = this.Height * this.Scale;

            float lineWidth = width / 113f;
            System.Drawing.Drawing2D.GraphicsState gs = g.Save();
            g.PageUnit = System.Drawing.GraphicsUnit.Millimeter;
            g.PageScale = 1;

            System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
            float xPosition = pt.X;

            System.Text.StringBuilder strbEAN13 = new System.Text.StringBuilder();
            System.Text.StringBuilder sbTemp = new System.Text.StringBuilder();

            float xStart = pt.X;
            float yStart = pt.Y;
            float xEnd = 0;

            System.Drawing.Font font = new System.Drawing.Font("Arial", this._fFontSize * this.Scale);

            this.CalculateChecksumDigit();

            sbTemp.AppendFormat("{0}{1}{2}{3}",
                this.CountryCode,
                this.ManufacturerCode,
                this.ProductCode,
                this.ChecksumDigit);


            string sTemp = sbTemp.ToString();

            string sLeftPattern = "";

            sLeftPattern = ConvertLeftPattern(sTemp.Substring(0, 7));

            strbEAN13.AppendFormat("{0}{1}{2}{3}{4}{1}{0}",
                this._sQuiteZone, this._sLeadTail,
                sLeftPattern,
                this._sSeparator,
                ConvertToDigitPatterns(sTemp.Substring(7), this._aRight));

            string sTempUPC = strbEAN13.ToString();

            float fTextHeight = g.MeasureString(sTempUPC, font).Height;

            for (int i = 0; i < strbEAN13.Length; i++)
            {
                if (sTempUPC.Substring(i, 1) == "1")
                {
                    if (xStart == pt.X)
                        xStart = xPosition;

                    if ((i > 12 && i < 55) || (i > 57 && i < 101))
                        g.FillRectangle(brush, xPosition, yStart, lineWidth, height - fTextHeight);
                    else
                        g.FillRectangle(brush, xPosition, yStart, lineWidth, height);
                }

                xPosition += lineWidth;
                xEnd = xPosition;
            }

            xPosition = xStart - g.MeasureString(this.CountryCode.Substring(0, 1), font).Width;
            float yPosition = yStart + (height - fTextHeight);

            g.DrawString(sTemp.Substring(0, 1), font, brush, new System.Drawing.PointF(xPosition, yPosition));

            xPosition += (g.MeasureString(sTemp.Substring(0, 1), font).Width + 43 * lineWidth) -
                (g.MeasureString(sTemp.Substring(1, 6), font).Width);

            g.DrawString(sTemp.Substring(1, 6), font, brush, new System.Drawing.PointF(xPosition, yPosition));

            xPosition += g.MeasureString(sTemp.Substring(1, 6), font).Width + (11 * lineWidth);

            g.DrawString(sTemp.Substring(7), font, brush, new System.Drawing.PointF(xPosition, yPosition));

            g.Restore(gs);
        }

        public System.Drawing.Bitmap CreateBitmap()
        {
            float tempWidth = (this.Width * this.Scale) * 100;
            float tempHeight = (this.Height * this.Scale) * 100;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap((int)tempWidth, (int)tempHeight);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bmp);
            this.DrawEan13Barcode(g, new System.Drawing.Point(0, 0));
            g.Dispose();
            return bmp;
        }

        private string ConvertLeftPattern(string sLeft)
        {
            switch (sLeft.Substring(0, 1))
            {
                case "0":
                    return CountryCode0(sLeft.Substring(1));

                case "1":
                    return CountryCode1(sLeft.Substring(1));

                case "2":
                    return CountryCode2(sLeft.Substring(1));

                case "3":
                    return CountryCode3(sLeft.Substring(1));

                case "4":
                    return CountryCode4(sLeft.Substring(1));

                case "5":
                    return CountryCode5(sLeft.Substring(1));

                case "6":
                    return CountryCode6(sLeft.Substring(1));

                case "7":
                    return CountryCode7(sLeft.Substring(1));

                case "8":
                    return CountryCode8(sLeft.Substring(1));

                case "9":
                    return CountryCode9(sLeft.Substring(1));

                default:
                    return "";
            }
        }

        private string CountryCode0(string sLeft)
        {
            return ConvertToDigitPatterns(sLeft, this._aOddLeft);
        }

        private string CountryCode1(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();

            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));

            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return sReturn.ToString();
        }

        private string CountryCode2(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return sReturn.ToString();
        }

        private string CountryCode3(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return sReturn.ToString();
        }

        private string CountryCode4(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return sReturn.ToString();
        }

        private string CountryCode5(string sLeft)
        { 
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return sReturn.ToString();
        }

        private string CountryCode6(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return sReturn.ToString();
        }

        private string CountryCode7(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aEvenLeft));
            return sReturn.ToString();
        }

        private string CountryCode8(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return sReturn.ToString();
        }

        private string CountryCode9(string sLeft)
        {
            System.Text.StringBuilder sReturn = new StringBuilder();
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(0, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(1, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(2, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(3, 1), this._aOddLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(4, 1), this._aEvenLeft));
            sReturn.Append(ConvertToDigitPatterns(sLeft.Substring(5, 1), this._aOddLeft));
            return sReturn.ToString();
        }

        private string ConvertToDigitPatterns(string inputNumber, string[] patterns)
        {
            System.Text.StringBuilder sbTemp = new StringBuilder();
            int iIndex = 0;
            for (int i = 0; i < inputNumber.Length; i++)
            {
                iIndex = Convert.ToInt32(inputNumber.Substring(i, 1));
                sbTemp.Append(patterns[iIndex]);
            }
            return sbTemp.ToString();
        }

        public void CalculateChecksumDigit()
        {
            string sTemp = this.CountryCode + this.ManufacturerCode + this.ProductCode;
            int iSum = 0;
            int iDigit = 0;

            for (int i = sTemp.Length; i >= 1; i--)
            {
                iDigit = Convert.ToInt32(sTemp.Substring(i - 1, 1));
                if (i % 2 == 0)
                {	
                    iSum += iDigit * 3;
                }
                else
                {
                    iSum += iDigit * 1;
                }
            }

            int iCheckSum = (10 - (iSum % 10)) % 10;
            this.ChecksumDigit = iCheckSum.ToString();

        }

        #region -- Attributes/Properties --

        public string Name
        {
            get
            {
                return _sName;
            }
        }

        public float MinimumAllowableScale
        {
            get
            {
                return _fMinimumAllowableScale;
            }
        }

        public float MaximumAllowableScale
        {
            get
            {
                return _fMaximumAllowableScale;
            }
        }

        public float Width
        {
            get
            {
                return _fWidth;
            }
            set
            {
                this._fWidth = value;
            }
        }

        public float Height
        {
            get
            {
                return _fHeight;
            }
            set
            {
                this._fHeight = value;
            }
        }

        public float FontSize
        {
            get
            {
                return _fFontSize;
            }
            set
            {
                this._fFontSize = value;
            }
        }

        public float Scale
        {
            get
            {
                return _fScale;
            }
            set
            {
                _fScale = 1.0f;
            }
        }

        public string CountryCode
        {
            get
            {
                return _sCountryCode;
            }
            set
            {
                while (value.Length < 1)
                {
                    value = "1" + value;
                }
                _sCountryCode = value;
            }
        }

        public string ManufacturerCode
        {
            get
            {
                return _sManufacturerCode;
            }
            set
            {
                _sManufacturerCode = value;
            }
        }

        public string ProductCode
        {
            get
            {
                return _sProductCode;
            }
            set
            {
                _sProductCode = value;
            }
        }

        public string ChecksumDigit
        {
            get
            {
                return _sChecksumDigit;
            }
            set
            {
                int iValue = Convert.ToInt32(value);
                if (iValue < 0 || iValue > 9)
                    throw new Exception("The Check Digit mst be between 0 and 9.");
                _sChecksumDigit = value;
            }
        }

        #endregion -- Attributes/Properties --

        public override string ToString()
        {
            this.CalculateChecksumDigit();
            return this.CountryCode + this.ManufacturerCode + this.ProductCode + this.ChecksumDigit;
        }
    }
}
