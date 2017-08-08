using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwinCAT.Ads;

namespace WindowsFormsLightDemo
{
	public partial class Form1 : Form
	{
		private TcAdsClient adsClient; 

		private int intStop;
		private int intStart;
		private int intStatus;

		private bool blStop;
		private bool blStart;
		private bool blStatus;
		public Form1()
		{
			InitializeComponent();
		}
		private void Form1_Load(object sender, System.EventArgs e)
		{
			try
			{
				adsClient = new TcAdsClient();

				// PLC1 Port -  TwinCAT 3=851
				adsClient.Connect(851);

				intStart =  adsClient.CreateVariableHandle("GVLLighting.StartLight");
				intStop = adsClient.CreateVariableHandle("GVLLighting.StopLight");
				intStatus = adsClient.CreateVariableHandle("GVLLighting.LightingStatus");
			}
			catch (Exception err)
			{ 
				MessageBox.Show(err.Message);
			}
		}
		private void button1_Click(object sender, EventArgs e)//Turnon
		{
			try
			{
				Form1_Load(sender, e);

				AdsStream adsStream = new AdsStream(30);
		//		AdsBinaryReader reader = new AdsBinaryReader(adsStream);
		//		adsClient.Read(intStart, adsStream);
				
				AdsStream adsStream2 = new AdsStream(30);
		//		AdsBinaryReader reader2 = new AdsBinaryReader(adsStream2);
		//		adsClient.Read(intStatus, adsStream2);

				AdsStream adsStream3 = new AdsStream(30);

		//		blStart = reader.ReadBoolean();
		//		blStatus = reader2.ReadBoolean();

			//	if (blStart == false)
			//	{
					label2.Text = adsClient.ReadAny(intStart, typeof(Boolean)).ToString();
					label3.Text = adsClient.ReadAny(intStop, typeof(Boolean)).ToString();
					label4.Text = adsClient.ReadAny(intStatus, typeof(Boolean)).ToString();

					blStart = true;
					blStatus = true;
					blStop = false;

					AdsBinaryWriter write = new AdsBinaryWriter(adsStream);
					adsClient.WriteAny(intStart, blStart);

					AdsBinaryWriter writer2 = new AdsBinaryWriter(adsStream2);
					adsClient.WriteAny(intStatus, blStatus);

					AdsBinaryWriter writer3 = new AdsBinaryWriter(adsStream3);
					adsClient.WriteAny(intStop, blStop);

					label1.Text = "Đã bật đèn";
					label5.Text = adsClient.ReadAny(intStart, typeof(Boolean)).ToString();
					label6.Text = adsClient.ReadAny(intStop, typeof(Boolean)).ToString();
					label7.Text = adsClient.ReadAny(intStatus, typeof(Boolean)).ToString();
				//	}
				//	else
				//blStatus = true;
				//		label1.Text = "TRUE nè";

				adsClient.Dispose();
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}

		private void button2_Click(object sender, EventArgs e)//Turnoff   --write
		{

			try
			{
				Form1_Load(sender, e);

				AdsStream adsStream = new AdsStream(30); 
				AdsStream adsStream2 = new AdsStream(30); 
				AdsStream adsStream3 = new AdsStream(30);

				label2.Text = adsClient.ReadAny(intStart, typeof(Boolean)).ToString();
				label3.Text = adsClient.ReadAny(intStop, typeof(Boolean)).ToString();
				label4.Text = adsClient.ReadAny(intStatus, typeof(Boolean)).ToString();

				blStart = false;
				blStatus = false;
				blStop = true;

				AdsBinaryWriter write = new AdsBinaryWriter(adsStream);
				adsClient.WriteAny(intStart, blStart);

				AdsBinaryWriter writer2 = new AdsBinaryWriter(adsStream2);
				adsClient.WriteAny(intStatus, blStatus);

				AdsBinaryWriter writer3 = new AdsBinaryWriter(adsStream3);
				adsClient.WriteAny(intStop, blStop);

				label1.Text = "Đã tắt đèn";
				label5.Text = adsClient.ReadAny(intStart, typeof(Boolean)).ToString();
				label6.Text = adsClient.ReadAny(intStop, typeof(Boolean)).ToString();
				label7.Text = adsClient.ReadAny(intStatus, typeof(Boolean)).ToString();


				adsClient.Dispose();
			}
			catch (Exception err)
			{
				MessageBox.Show(err.Message);
			}
		}
	}
}
