using EntityWebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WebAPI.DAL;

namespace WApiPalletDAL
{
    public class PalletDAL : BaseDAL<PalletInfo>
    {
        //Apenas altera o Status para 1 que representa cancelado.
        public bool DeletePalletById(int codPallet)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[1];
                    Parameters[0] = new SqlParameter("@CodPallet", codPallet);
                    
                    return Convert.ToBoolean(ExecuteNonQuery("usp_DeletarPallet", System.Data.CommandType.StoredProcedure, Parameters));
                }
            }
            catch
            {
                throw;
            }
        }
        //Faz o Update nos outros campos através do CodPallet.
        public bool Update(PalletInfo voPallet)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[6];
                    Parameters[0] = new SqlParameter("@Caderno", voPallet.CodPallet);
                    Parameters[1] = new SqlParameter("@Caderno", voPallet.Caderno);
                    Parameters[2] = new SqlParameter("@TipoPallet", voPallet.TipoPallet);
                    Parameters[3] = new SqlParameter("@Operador", voPallet.Operador);
                    Parameters[4] = new SqlParameter("@QtdeExemplares", voPallet.QtdeExemplares);
                    Parameters[5] = new SqlParameter("@StatusPallet", voPallet.StatusPallet);

                    return Convert.ToBoolean(ExecuteNonQuery("usp_UpdatePallet", System.Data.CommandType.StoredProcedure, Parameters));
                }
            }
            catch
            {
                throw;
            }
        }
        //CodPallet é identity e o  Default do Status Ativo, por isso não são passados como parâmetro. 
        public bool Insert(PalletInfo voPallet)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[4];
                    Parameters[0] = new SqlParameter("@Caderno", voPallet.Caderno);
                    Parameters[1] = new SqlParameter("@TipoPallet", voPallet.TipoPallet);
                    Parameters[2] = new SqlParameter("@Operador", voPallet.Operador);
                    Parameters[3] = new SqlParameter("@QtdeExemplares", voPallet.QtdeExemplares);

                    return Convert.ToBoolean(ExecuteNonQuery("usp_InserirPallet", System.Data.CommandType.StoredProcedure, Parameters));
                }
            }
            catch
            {
                throw;
            }

        }

        public List<PalletInfo> GetAll()
        {
            List<PalletInfo> lstPallet = new List<PalletInfo>();
            DataTable dt;
            try
            {
                using(Connection)
                {
                    dt=ExecuteDataTable("usp_ObterPallets", System.Data.CommandType.StoredProcedure);
                    lstPallet = ConverterParaLista<PalletInfo>(dt);                    
                }
                return lstPallet;
            }
            catch
            {
                throw;
            }
        }
    }
}
