using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using MVCSmartAPI01.Models;

using System.Data.Entity.Validation;

namespace MVCSmartAPI01.DataAccessRepository
{
    public class DashboardRep
    {
        //The dendency for the DbContext specified the current class. 
        [Dependency]
        public DB_SMARTEntities1 ctx { get; set; }

        //Get all Data
        public IEnumerable<dashFeeByRekanan> FeeByRekanan(int Tahun, int TopN, int TypeOfRekanan)
        {
            //return ctx.fDashFeeByRekanan(Tahun, TopN, TypeOfRekanan);
            IEnumerable<dashFeeByRekanan> ListFeeByRek = new List<dashFeeByRekanan>();

            ListFeeByRek = (from listFee in ctx.fDashFeeByRekananFinal(Tahun, TopN, TypeOfRekanan)
                            select new dashFeeByRekanan
                            {
                                IdSupervisor = listFee.IdSupervisor,
                                KelasRangking = listFee.Ranking,
                                NamaRekanan = listFee.NamaRekanan,
                                TotalFee = listFee.TotalFee,
                                TotalFeeAll = 0,
                                Persen = 0
                            }).ToList().OrderBy(x => x.IdSupervisor).ThenBy(x => x.KelasRangking);
            //switch (TypeOfRekanan)
            //{
            //    case 1:
            //    case 2:
            //    case 3:
            //    case 7:
            //        ListFeeByRek = (from listFee in ctx.fDashFeeByRekanan1237(Tahun, TopN, TypeOfRekanan)
            //                        select new dashFeeByRekanan
            //                       {
            //                           KelasRangking = listFee.KelasRangking,
            //                           NamaRekanan = listFee.NamaRekanan,
            //                           TotalFee = listFee.TotalFee,
            //                           TotalFeeAll = listFee.TotalFeeAll,
            //                           Persen = listFee.Persen
            //                       }).ToList().OrderBy(x => x.KelasRangking);
            //        break;
            //    case 4:
            //    case 5:
            //        ListFeeByRek = (from listFee in ctx.fDashFeeByRekanan45(Tahun, TopN, TypeOfRekanan)
            //                        select new dashFeeByRekanan
            //                       {
            //                           KelasRangking = listFee.KelasRangking,
            //                           NamaRekanan = listFee.NamaRekanan,
            //                           TotalFee = listFee.TotalFee,
            //                           TotalFeeAll = listFee.TotalFeeAll,
            //                           Persen = listFee.Persen
            //                       }).ToList().OrderBy(x => x.KelasRangking);
            //        break;
            //    case 6:
            //        ListFeeByRek = (from listFee in ctx.fDashFeeByRekanan6(Tahun, TopN, TypeOfRekanan)
            //                        select new dashFeeByRekanan
            //                       {
            //                           KelasRangking = listFee.KelasRangking,
            //                           NamaRekanan = listFee.NamaRekanan,
            //                           TotalFee = listFee.TotalFee,
            //                           TotalFeeAll = listFee.TotalFeeAll,
            //                           Persen = listFee.Persen
            //                       }).ToList().OrderBy(x => x.KelasRangking);
            //        break;

            //    default:
            //        break;
            //}
            return ListFeeByRek;
        }
        public IEnumerable<dashPekerjaanByRekanan> PekerjaanByRekanan(int Tahun, int TopN, int TypeOfRekanan)
        {
            //return ctx.fDashPekerjaanByRekanan(Tahun, TopN, TypeOfRekanan);
            IEnumerable<dashPekerjaanByRekanan> ListPekerjaanByRek = new List<dashPekerjaanByRekanan>();
            ListPekerjaanByRek = (from listFee in ctx.fDashPekerjaanByRekananFinal(Tahun, TopN, TypeOfRekanan)
                            select new dashPekerjaanByRekanan
                            {
                                IdSupervisor = listFee.IdSupervisor,
                                KelasRangking = listFee.Ranking,
                                NamaRekanan = listFee.NamaRekanan,
                                JumlahPekerjaan = listFee.JmlProyek,
                                AllPekerjaan = 0,
                                Persen = 0
                            }).ToList().OrderBy(x => x.IdSupervisor).ThenBy(x => x.KelasRangking);

            //switch (TypeOfRekanan)
            //{
            //    case 1:
            //    case 2:
            //    case 3:
            //    case 7:
            //        ListFeeByRek = (from listFee in ctx.fDashPekerjaanByRekananFinal(Tahun, TopN, TypeOfRekanan)
            //                        select new dashPekerjaanByRekanan
            //                        {
            //                            KelasRangking = listFee.Ranking,
            //                            NamaRekanan = listFee.NamaRekanan,
            //                            JumlahPekerjaan = listFee.JmlProyek,
            //                            AllPekerjaan = 0,
            //                            Persen = 0
            //                        }).ToList().OrderBy(x => x.KelasRangking);
            //        break;
            //    case 4:
            //    case 5:
            //        ListFeeByRek = (from listFee in ctx.fDashPekerjaanByRekanan45(Tahun, TopN, TypeOfRekanan)
            //                        select new dashPekerjaanByRekanan
            //                        {
            //                            KelasRangking = listFee.KelasRangking,
            //                            NamaRekanan = listFee.NamaRekanan,
            //                            JumlahPekerjaan = listFee.JumlahPekerjaan,
            //                            AllPekerjaan = listFee.AllPekerjaan,
            //                            Persen = listFee.Persen
            //                        }).ToList().OrderBy(x => x.KelasRangking);
            //        break;
            //    case 6:
            //        ListFeeByRek = (from listFee in ctx.fDashPekerjaanByRekanan6(Tahun, TopN, TypeOfRekanan)
            //                        select new dashPekerjaanByRekanan
            //                        {
            //                            KelasRangking = listFee.KelasRangking,
            //                            NamaRekanan = listFee.NamaRekanan,
            //                            JumlahPekerjaan = listFee.JumlahPekerjaan,
            //                            AllPekerjaan = listFee.AllPekerjaan,
            //                            Persen = listFee.Persen
            //                        }).ToList().OrderBy(x => x.KelasRangking);
            //        break;

            //    default:
            //        break;
            //}
            return ListPekerjaanByRek;
        }
        public IEnumerable<mstRekananMap> LatLongByRekanan(int TypeOfRekanan)
        {
            //var arrLatLong = ctx.mstRekanans.Where(x => x.IdTypeOfRekanan.Equals(TypeOfRekanan));
            var colorCode = from colCode in ctx.mstReferences
                            where colCode.RefType.Equals("ClassColor")
                            select new {
                                refCode = colCode.RefCode,
                                refDesc = colCode.RefDesc
                            };
            var mapData = from mstRek in ctx.mstRekanan
                          join clColor in colorCode on mstRek.ClassOfRekanan.ToString() equals clColor.refCode 
                          where mstRek.IdTypeOfRekanan.Equals(TypeOfRekanan)
                          select new { mstRek.IdRekanan, mstRek.Name, mstRek.Latitude, mstRek.Longitude, mstRek.ClassOfRekanan, clColor.refDesc };
            IEnumerable<mstRekananMap> listData =
                from myData in mapData.AsEnumerable()
                select new mstRekananMap(myData.IdRekanan, myData.Name, myData.Latitude, myData.Longitude, myData.refDesc);
            return listData;
        }

    }
}