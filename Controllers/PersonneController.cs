using Appli.MVC.Entity.Models;
using DAL3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Appli.MVC.Entity.Controllers
{
    public class PersonneController : Controller
    {
        private DAL3.V2B_EntityEntities contexteEF = new DAL3.V2B_EntityEntities();
        // on crée un contexte EF dans lequel nos tables sont crées
        public ActionResult Liste()
        {
            List<Personne> personnes = contexteEF.Personne.ToList();
            //on recupère dans le contexte de notre EF la table personne, on la convertie
            //en liste et on l'affecte à la variable personnes
            return View(personnes);
        }

        [HttpGet]
        /*la création et l'édition d'une personne sont beauccoup semblabes
         on rend donc l'edit nullable, s'il à une valeur c'est l'édition sinon
        c'est une création de nouvelle personne*/
        public ActionResult Edit(int? id)
        {
            if(id.HasValue)
            {
                Personne personne = contexteEF.Personne.Single(p => p.ID == id);
                //Mapping
                PersonneEditee personneEditee = AutoMapper.Mapper.Map<PersonneEditee>(personne);

                return View(personneEditee);
            }
            else
            {
                //pas d'ID : création
                return View(new PersonneEditee());
            }
            

        }
        /*On veut à présent enregistrer les modifications apportées au niveau de Edit
         à notre base de donné*/
        [HttpPost]
        public ActionResult Edit(PersonneEditee personne)
        {
            if (!ModelState.IsValid)
            {
                return View(personne);
            }
            if (personne.ID.HasValue)
            {
                // on recupere la personne de la base à modifier 
                Personne personneDB = contexteEF.Personne.Single(p => p.ID == personne.ID);
                //Mapping pour propager les modifications de la personne de la base récupérée vers 
                // la base
                personneDB = AutoMapper.Mapper.Map<PersonneEditee, Personne>(personne, personneDB);
            }
            else
            {
                // on crée une nouvelle personne qui aura les attributs de Personne
                var nouvellePersonne = AutoMapper.Mapper.Map<Personne>(personne);
                //on lui affecte un ID
                int idMax = contexteEF.Personne.Max(p => p.ID);
                nouvellePersonne.ID = idMax + 1;
                //on ajoute nvlle personne à Personne de Personne de la table
                contexteEF.Personne.Add(nouvellePersonne);
            }
            contexteEF.SaveChanges();
            return RedirectToAction("Liste");
        }

        // code pour réagir au clic sur la page web pour supprimer une personne
        [HttpPost]
        public JsonResult Delete(int id)
        {
            Personne personne = contexteEF.Personne.Single(p => p.ID == id);
            contexteEF.Personne.Remove(personne);
            contexteEF.SaveChanges();

            return Json(new { Suppression = "OK"});
        }

    }
}