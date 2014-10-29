﻿using MiniBD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Combaxe___New.classes.services
{
    class EnnemiService
    {
        BdService bdCombaxe = new BdService();

        /// <summary>
        /// Méthode qui retourne toutes les informations de l'ennemi (non, niveau, image, ses compétences, ses caractéristiques) - William Themens
        /// </summary>
        /// <param name="idEnnemi">Permet de savoir quel ennemi est recherché</param>
        /// <returns></returns>
        public Ennemi RetrieveInfoEnnemi(string idEnnemi)
        {
            string selConnexion = "SELECT idEnnemi, idInventaire, nom, niveau, image FROM Ennemis WHERE idEnnemi = '" + idEnnemi + "' AND boss = false;";
            List<string>[] unEnnemi;

            int nombreRange = 0;
            unEnnemi = bdCombaxe.selection(selConnexion, 5, ref nombreRange);

            //On va chercher les caractéristiques de l'ennemi
            CaracteristiqueService caracteristiqueService = new CaracteristiqueService();
            List<Caracteristique> caracteristiques = new List<Caracteristique>();
            caracteristiques = caracteristiqueService.RetrieveCaracteristiqueEnnemi(idEnnemi);

            //Selection des idCompetence pour trouver les compétences de l'ennemi
            selConnexion = "SELECT idCompetence FROM competencesEnnemis WHERE idEnnemi = '" + idEnnemi + "';";
            List<string>[] idCompetences;
            idCompetences = bdCombaxe.selection(selConnexion, 1, ref nombreRange);

            //Selection qui va chercher les informations des competences
            Competence uneCompetence;
            List<Competence> lesCompetences = new List<Competence>();
            CompetenceService competenceService = new CompetenceService();

            for (int i = 0; i < idCompetences.Count(); i++)
            {
                uneCompetence = competenceService.RetrieveInfoCompetence(idCompetences[i][0]);
                lesCompetences.Add(uneCompetence);
            }

            //On insere les competences dans l'ennemi
            Ennemi ennemi = new Ennemi(Convert.ToInt32(unEnnemi[0][0]), Convert.ToInt32(unEnnemi[0][3]), unEnnemi[0][4], unEnnemi[0][2],caracteristiques, lesCompetences);

            return ennemi;
        }

        /// <summary>
        /// Fonction qui retourne le ID de tous les ennemis
        /// </summary>
        /// <returns></returns>
        public List<string>[] RetrieveIdTousEnnemis()
        {
            string selConnexion = "SELECT idEnnemi FROM Ennemis WHERE boss = false;";
            List<string>[] tousEnnemi;

            int nombreRange = 0;
            tousEnnemi = bdCombaxe.selection(selConnexion, 1, ref nombreRange);

            return tousEnnemi;
        }
    }
}