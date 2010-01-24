using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Mongdio.code
{
	class RTFHelper
	{
		public const string COLOR_TABLE =
			"{\\colortbl ;\\red0\\green128\\blue0;\\red255\\green0\\blue0;\\red0\\green0\\blue255;}";
		
		public static void SetRTF(RichTextBox rtb, string rtf, string colorTable)
		{
			rtb.Clear();
			rtb.Text = "rtfing...";
			var orgRtf = rtb.Rtf;
			rtb.Clear();

			orgRtf = InsertColorTable(orgRtf, colorTable);
			orgRtf = orgRtf.Replace("rtfing...", rtf);
			rtb.Rtf = orgRtf;
		}

		private static string InsertColorTable(string rtf, string colorTable)
		{
			// Search for colour table info, if it exists (which it shouldn't)
			// remove it and replace with our one
			int iCTableStart = rtf.IndexOf("colortbl;");

			if(iCTableStart != -1) //then colortbl exists
			{
				//find end of colortbl tab by searching
				//forward from the colortbl tab itself
				int iCTableEnd = rtf.IndexOf('}', iCTableStart);
				rtf = rtf.Remove(iCTableStart, iCTableEnd - iCTableStart);

				//now insert new colour table at index of old colortbl tag
				rtf = rtf.Insert(iCTableStart, colorTable );
			}
			//colour table doesn't exist yet, so let's make one
			else
			{
				// find index of start of header
				int iRTFLoc = rtf.IndexOf("\\rtf");
				// get index of where we'll insert the colour table
				// try finding opening bracket of first property of header first                
				int iInsertLoc = rtf.IndexOf('{', iRTFLoc);

				// if there is no property, we'll insert colour table
				// just before the end bracket of the header
				if(iInsertLoc == -1) iInsertLoc = rtf.IndexOf('}', iRTFLoc) - 1;

				// insert the colour table at our chosen location                
				rtf = rtf.Insert(iInsertLoc, colorTable );
			}

			return rtf;
		}
	}
}
