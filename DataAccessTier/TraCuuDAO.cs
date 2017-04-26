﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DTO;

namespace DataAccessTier
{
    public class TraCuuDAO : DatabaseConnection
    {
        public TraCuuDAO() : base() { }
        
        public DataTable LayDanhSachChuyenBayTheoMaChuyenBay(string MaChuyenBay)
        {
            string query = "SELECT * FROM CHUYENBAY a JOIN CHITIETCHUYENBAY b ON a.MACHUYENBAY = b.MACHUYENBAY JOIN TUYENBAY c ON a.MATUYENBAY = c.MATUYENBAY JOIN SANBAY d ON c.MASANBAY = d.MASANBAY WHERE a.MACHUYENBAY = @MACHUYENBAY";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@MACHUYENBAY"] = MaChuyenBay;
            return ExecuteQuery(query, parameters);            
        }

        public DataTable LayDanhSachChuyenBayTheoTuyenBay(TuyenBay tuyenbay)
        {
            string query = "SELECT CB.MACHUYENBAY ,SANBAYDI, SANBAYDEN, NGAYGIO, THOIGIANBAY, SUM(SOGHETRONG) SOGHETRONG, SUM(SOGHEDAT) SOGHEDAT FROM ( SELECT MACHUYENBAY, MATUYENBAY, NGAYGIO, THOIGIANBAY FROM CHUYENBAY WHERE MATUYENBAY = ( SELECT MATUYENBAY FROM TUYENBAY WHERE SANBAYDI = @SanBayDi AND SANBAYDEN = @SanBayDen)) CB, ( SELECT * FROM TUYENBAY WHERE SANBAYDI = @SanBayDi AND SANBAYDEN = @SanBayDen) TB, TINHTRANGVE T WHERE CB.MACHUYENBAY = T.MACHUYENBAY GROUP BY CB.MACHUYENBAY, SANBAYDI, SANBAYDEN, NGAYGIO, THOIGIANBAY";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@SanBayDi"] = tuyenbay.MaSanBayDi;
            parameters["@SanBayDen"] = tuyenbay.MaSanBayDen;
            return ExecuteQuery(query, parameters);            
        }

        public DataTable LayDanhSachSanBay()
        {
            string query = "SELECT * FROM SANBAY";
            return ExecuteQuery(query);
        }        

        public DataTable LayDanhSachSanBayDenTheoSanBayDi(string MaSanBayDi)
        {
            string query = "SELECT a.MASANBAY, a.TENSANBAY FROM SANBAY a JOIN TUYENBAY b ON a.MASANBAY = b.SANBAYDEN WHERE b.SANBAYDI = @MaSanBayDi";
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters["@MaSanBayDi"] = MaSanBayDi;
            return ExecuteQuery(query, parameters);
        }

        public DataTable LayThongTinTatCaChuyenBay()
        {
            string query = "SELECT C.MACHUYENBAY , SANBAYDI , SANBAYDEN , NGAYGIO , THOIGIANBAY , SUM(SOGHETRONG) , SUM(SOGHEDAT)  FROM CHUYENBAY C, TINHTRANGVE T, TUYENBAY TB WHERE C.MATUYENBAY = TB.MATUYENBAY AND C.MACHUYENBAY = T.MACHUYENBAY GROUP BY C.MACHUYENBAY, C.NGAYGIO, C.THOIGIANBAY, TB.SANBAYDI, TB.SANBAYDEN";
            return ExecuteQuery(query);
        }

    }
}
