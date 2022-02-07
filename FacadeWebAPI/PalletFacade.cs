using EntityWebAPI;
using System;
using System.Collections.Generic;
using System.Text;
using WApiPalletBUS;

namespace WApiPalletFacade
{
    public  class PalletFacade
    {
        public static bool Insert(PalletInfo voPallet)
        {
            PalletBUS oBus = new PalletBUS();
            return oBus.Insert(voPallet);
        }
        public static bool Update(PalletInfo voPallet)
        {
            PalletBUS oBus = new PalletBUS();
            return oBus.Update(voPallet);
        }
        public static bool DeletePalletById(int codPallet)
        {
            PalletBUS oBus = new PalletBUS();
            return oBus.DeletePalletById(codPallet);
        }
        public static List<PalletInfo> GetAll()
        {
            PalletBUS oBUS = new PalletBUS();
            return oBUS.GetAll();
        }
    }
}
