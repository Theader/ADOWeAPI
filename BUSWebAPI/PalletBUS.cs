using EntityWebAPI;
using System;
using System.Collections.Generic;
using System.Text;
using WApiPalletDAL;

namespace WApiPalletBUS
{
    public class PalletBUS
    {
        public bool Insert(PalletInfo voPallet)
        {
            PalletDAL oDAL = new PalletDAL();
            return oDAL.Insert(voPallet);
        }
        public bool Update(PalletInfo voPallet)
        {
            PalletDAL oDAL = new PalletDAL();
            return oDAL.Update(voPallet);
        }
        public bool DeletePalletById(int codPallet)
        {
            PalletDAL oDAL = new PalletDAL();
            return oDAL.DeletePalletById(codPallet);
        }
        public List<PalletInfo> GetAll()
        {
            PalletDAL oDAL = new PalletDAL();
            return oDAL.GetAll();
        }
    }
}
