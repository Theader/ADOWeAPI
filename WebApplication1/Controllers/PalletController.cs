using EntityWebAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WApiPalletFacade;

namespace WebApplication1.Controllers
{
    public class PalletController : Controller
    {
        [HttpGet]
        [Route("PalletController/GetAll")]
        public ActionResult GetAll()
        {
            try
            {
                return Ok(PalletFacade.GetAll());
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("PalletController/Inserir")]
        public ActionResult Inserir([FromBody]PalletInfo voPallet)
        {
            try
            {
                if(PalletFacade.Insert(voPallet))
                    return Ok("Pallet Inserido com Sucesso.");
                else
                    return BadRequest("Não foi possível inserir o pallet, verifique se todos os campos estão preenchidos.");
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        [Route("PalletController/Update")]
        public ActionResult Update(PalletInfo voPallet)
        {
            try
            {
                if (PalletFacade.Update(voPallet))
                    return Ok();
                else
                    return BadRequest("Não foi possível atualizar este pallet,verifique com o administrador.");
            }
            catch
            {
                throw;
            }
        }

        [HttpDelete("{CodPallet}")]
        [Route("PalletController/Delete/{CodPallet}")]
        public ActionResult Delete(int CodPallet)
        {
            try
            {                
                if (PalletFacade.DeletePalletById(CodPallet))
                    return Ok("Pallet cancelado com sucesso.");
                else
                    return BadRequest("Não foi encontrado nenhum pallet com esse código.");
            }
            catch
            {
                throw;
            }
        }
    }
}
