using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AvisFormation.Data;
using AvisFormation.WebUi.Models;

namespace AvisFormation.WebUi.Controllers
{
    public class FormationController : Controller
    {
        // GET: Formation
        public ActionResult ToutesLesFormations()
        {
            var listFormations = new List<Formation>();
            using (var context = new AvisFormationDbEntities())
            {
                listFormations = context.Formations.ToList();
                foreach (var formationEntity in listFormations)
                {
                    var vm = new FormationAvecAvisViewModel();
                    vm.Nom = formationEntity.Nom;
                    vm.Id = formationEntity.Id;
                    vm.Url = formationEntity.Url;
                    vm.Description = formationEntity.Description;
                    vm.NomSeo = formationEntity.NomSeo;

                    if (formationEntity.Avis.Count > 0)
                    {
                        vm.Note = formationEntity.Avis.Average(a => a.Note);
                        vm.NombreAvis = formationEntity.Avis.Count;
                    }
                    else
                    {
                        vm.Note = 0;
                        vm.NombreAvis = 0;
                    }
                    vm.Avis = formationEntity.Avis.ToList();
                }

            }
            return View(listFormations);
        }
        public ActionResult DetailsFormations(string nomSeo)
        {
            var vm = new FormationAvecAvisViewModel();
            var formationEntity = new Formation();
            using (var context = new AvisFormationDbEntities())
            {
                formationEntity = context.Formations.Where(f => f.NomSeo == nomSeo).FirstOrDefault();
                if (formationEntity == null)
                    return RedirectToAction("Index", "Home");
                vm.Nom = formationEntity.Nom;
                vm.Id = formationEntity.Id;
                vm.Url = formationEntity.Url;
                vm.Description = formationEntity.Description;
                vm.NomSeo = formationEntity.NomSeo;

                if (formationEntity.Avis.Count > 0)
                {
                    vm.Note = formationEntity.Avis.Average(a => a.Note);
                    vm.NombreAvis = formationEntity.Avis.Count;
                }
                else
                {
                    vm.Note = 0;
                    vm.NombreAvis = 0;
                }
                vm.Avis = formationEntity.Avis.ToList();
            }
            return View(vm);
        }
    }

    
}