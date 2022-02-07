using EntityWebAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using WebAPI.DAL;

namespace DALWebAPI
{
    public class OpDAL : BaseDAL<OpInfo>
    {
        // Setar o Status para Cancelado
        public bool DeleteOpByOpAtividade(PalletInfo OpAtiv)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[2];
                    Parameters[0] = new SqlParameter("@NumOrdem", OpAtiv.NumOrdem);
                    Parameters[1] = new SqlParameter("@NumOrdem", OpAtiv.Atividade);

                    return Convert.ToBoolean(ExecuteNonQuery("usp_DeletarOpAtividade", System.Data.CommandType.StoredProcedure, Parameters));
                }
            }
            catch
            {
                throw;
            }
        }
        //Update Através da Op e Atividade, Atividade não é obrigatório na proc, utilizado apenas para o usuário encerrar/cancelar a Op e todas as atividades de uma só vez. 
        public bool Update(OpInfo OpAtiv)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[5];
                    Parameters[0] = new SqlParameter("@NumOrdem", OpAtiv.NumOrdem);
                    Parameters[1] = new SqlParameter("@Atividade", OpAtiv.Atividade);
                    Parameters[2] = new SqlParameter("@Produto", OpAtiv.Produto);
                    Parameters[4] = new SqlParameter("@StatusOp", OpAtiv.StatusOp);

                    return Convert.ToBoolean(ExecuteNonQuery("usp_UpdateOpAtiv", System.Data.CommandType.StoredProcedure, Parameters));
                }
            }
            catch
            {
                throw;
            }
        }
        //Status é default Ativo e data cadastro é getdate()
        public bool Insert(OpInfo voOp)
        {
            try
            {
                using (Connection)
                {
                    Parameters = new SqlParameter[3];
                    Parameters[0] = new SqlParameter("@NumOrdem", voOp.NumOrdem);
                    Parameters[1] = new SqlParameter("@Atividade", voOp.Atividade);
                    Parameters[2] = new SqlParameter("@Produto", voOp.Produto);           

                    return Convert.ToBoolean(ExecuteScalar("usp_InsertOp", System.Data.CommandType.StoredProcedure, Parameters));
                }
            }
            catch
            {
                throw;
            }

        }

        public List<OpInfo> GetAll()
        {
            List<OpInfo> lstOps = new List<OpInfo>();
            DataTable dt;
            try
            {
                using (Connection)
                {
                    dt = ExecuteDataTable("usp_ObterOps", System.Data.CommandType.StoredProcedure);
                    lstOps = ConverterParaLista<OpInfo>(dt);
                }
                return lstOps;
            }
            catch
            {
                throw;
            }
        }
    }
}
