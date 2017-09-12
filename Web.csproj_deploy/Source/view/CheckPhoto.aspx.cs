using System;
using System.IO;
using System.Drawing;

namespace SanZi.Web
{
    public partial class CheckPhoto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 取随机数
        /// </summary>
        /// <param name="startNum"></param>
        /// <param name="endNum"></param>
        /// <returns></returns>
        public static int getRandNumInt(int startNum, int endNum)
        {
            int num = 0;
            Random rand = new Random();
            num = rand.Next(startNum, endNum);
            return num;

        }

        /// <summary>
        /// 判断图片颜色
        /// </summary>
        /// <param name="red"></param>
        /// <param name="green"></param>
        /// <param name="blue"></param>
        /// <returns></returns>
        public bool checkPicRGB(int red, int green, int blue)
        {
            bool retResult = false;
            //票据判断标准 120<red<200  190<green<210  180<blue<210
            //if ((red > 120 && red < 200) && (green > 190 && green < 210) && (blue > 180 && blue < 210))
            if (red > 140 && red > 180 && blue > 140)
            {
                retResult = true;
            }
            else
            {
                retResult = false;
            }
            return retResult;
        }

        /// <summary>
        /// 基本思路：在图片上随机选取N个像素点，判断每个点颜色
        /// 通过符合点数占总数的比例判读是否为正常票据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCheckPhoto_Click(object sender, EventArgs e)
        {
            string picAddr = Server.MapPath("./images/d.jpg").ToString();
            this.lblResult.Text = picAddr;

            if (File.Exists(picAddr))
            {

                string filename = System.IO.Path.GetFileName(picAddr);//文件名 
                string extension = System.IO.Path.GetExtension(picAddr);//扩展名 
                string fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(picAddr);// 没有扩展名的文件名 “Default”

                int picWidth = 0;//图片长度
                int picHeight = 0;//图片高度
                System.Drawing.Image img = System.Drawing.Image.FromFile(picAddr);
                picWidth = img.Width;
                picHeight = img.Height;

                int checkPointNum = 10;//取点数量 取10个点，有3个符合及为合法票据
                int successNum = 0;//符合要求的点数
                int xAddr = 0, yAddr = 0;//坐标位置
                for (int i = 0; i < checkPointNum; i++)
                {
                    xAddr = getRandNumInt(100, picWidth - 100);//像素点坐标值100<x<图片宽度
                    yAddr = getRandNumInt(140, picHeight - 140);//像素点坐标值140<y<图片高度

                    using (Bitmap bmp = new Bitmap(picAddr))
                    {
                        Color pixelColor = bmp.GetPixel(xAddr, yAddr);
                        int alpha = pixelColor.A;//像素点颜色的 Alpha 值
                        int red = pixelColor.R;//颜色的 RED 分量值
                        int green = pixelColor.G;//颜色的 GREEN 分量值
                        int blue = pixelColor.B;//颜色的 BLUE 分量值
                        if (checkPicRGB(red, green, blue))
                        {
                            successNum = successNum + 1;
                        }
                    }
                }

                if (successNum > 3)
                {
                    LTP.Common.MessageBox.Show(this.Page, "票据图片符合要求！");
                }
                else
                {
                    LTP.Common.MessageBox.Show(this.Page, "票据图片不符合要求");
                }

            }
            else
            {
                LTP.Common.MessageBox.Show(this.Page, "票据图片不存在");
            }
        }
    }
}
