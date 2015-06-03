<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BBukuIndukRpt.ascx.cs" Inherits="CodeX.Muses.Web.StudentManagement.Report.BBukuIndukRpt" %>

<div id="divReportHeader" runat="server">
    <style type="text/css">
         .lblHeader {font-weight:bold;}
         h4         { margin-bottom: 0px; font-size: 1.1em }
    </style>
</div>

<div id="divReportBody" runat="server">
    <style type="text/css">
        .tblRapor tr td { padding:5px 3px;}   
        .tblRapor tr td { border-top: 1px solid; border-left:1px solid; }   
        .tblRapor        { border-right:1px solid; border-bottom: 1px solid;}        
        .tdScore { width:50px; }
        table tr td {padding:3px;}
    </style>
    <div style="text-align:center">
        <h1>IV. LEMBAR BUKU INDUK REGISTER</h1>
        NIS/NISN:{NISN}
    </div>
    <table class='tblReport' style='width:100%;margin-top: 15px' cellpadding='0' cellspacing='0'>
        <tbody class="reportBody">
            <tr class="trReportBody">
                <td valign="top">
                    <h3 style="font-weight:bold;">A. KETERANGAN PRIBADI</h3>
                    <div style="padding-left:10px;" id="divPersonal" runat="server">
                        <table cellpadding='0' cellspacing='0'>
                            <colgroup>
                                <col width="20px;" />
                                <col width="170px" />
                                <col width="20px" />
                                <col />
                            </colgroup>
                            <tr>
		                        <td>1.</td>
		                        <td>Nama Perserta didik</td>
		                        <td></td>
		                        <td></td>
	                        </tr>
	                        <tr>
		                        <td></td>
		                        <td>a. Lengkap</td>
		                        <td>:</td>
		                        <td>{Fullname}</td>
	                        </tr>
	                        <tr>
		                        <td></td>
		                        <td>b. Panggilan</td>
		                        <td>:</td>
		                        <td>{PreferredName}</td>
	                        </tr>
	                        <tr>
		                        <td>2.</td>
		                        <td>Jenis Kelamin</td>
		                        <td>:</td>
		                        <td>{Gender}</td>
	                        </tr>
	                        <tr>
		                        <td>3.</td>
		                        <td>Tempat Lahir</td>
		                        <td>:</td>
		                        <td>{CityOfBirth}</td>
	                        </tr>
                            <tr>
		                        <td>4.</td>
		                        <td>Tanggal Lahir</td>
		                        <td>:</td>
		                        <td>{DateOfBirth}</td>
	                        </tr>
                            <tr>
		                        <td>5.</td>
		                        <td>Agama</td>
		                        <td>:</td>
		                        <td>{Religion}</td>
	                        </tr>
                            <tr>
		                        <td>6.</td>
		                        <td>Kewarganegaraan</td>
		                        <td>:</td>
		                        <td>{Nationality}</td>
	                        </tr>
                            <tr>
		                        <td>7.</td>
		                        <td>Anak ke berapa</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>8.</td>
		                        <td>Jumlah saudara kandung</td>
		                        <td>:</td>
		                        <td>{Sibling}</td>
	                        </tr>
                            <tr>
		                        <td>9.</td>
		                        <td>Jumlah saudara angkat</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>10.</td>
		                        <td>Jumlah saudara angkat</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>11.</td>
		                        <td>Anak yatim/piatu/yatim piatu</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>12.</td>
		                        <td>Bahasa sehari-hari di rumah</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td valign="top">
                    <h3 style="font-weight:bold;">B. KETERANGAN TEMPAT TINGGAL</h3>
                    <div style="padding-left:10px;" id="divAddress" runat="server">
                        <table cellpadding='0' cellspacing='0'>
                            <colgroup>
                                <col width="20px" />
                                <col width="170px"/>
                                <col width="20px"/>
                                <col />
                            </colgroup>
                            <tr>
		                        <td>13.</td>
		                        <td>Alamat</td>
		                        <td></td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>a. Jalan/Gang</td>
		                        <td>:</td>
		                        <td>{StreetName}</td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>b. Nomor Rumah, RT/RW</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>c. Kelurahan dan kecamatan</td>
		                        <td>:</td>
		                        <td>{District}</td>
	                        </tr>
                            <tr>
		                        <td>14.</td>
		                        <td>Nomor telepon rumah</td>
		                        <td>:</td>
		                        <td>{HomePhone}</td>
	                        </tr>
                            <tr>
		                        <td>15.</td>
		                        <td>Tempat tinggal tersebut adalah</td>
		                        <td></td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td colspan="3">
                                    <table>
                                        <tr>
                                            <td style="border:1px solid; width:20px;"></td>
                                            <td>Pada orang tua kandung</td>
                                            <td>&nbsp;</td>
                                            <td style="border:1px solid; width:20px;"></td>
                                            <td>Asrama</td>
                                        </tr>
                                        <tr>
                                            <td style="border:1px solid; width:20px;"></td>
                                            <td>Pada saudara</td>
                                            <td>&nbsp;</td>
                                            <td style="border:1px solid; width:20px;"></td>
                                            <td>Lain-lain</td>
                                        </tr>
                                    </table>
                                </td>
	                        </tr>
                            <tr>
		                        <td>16.</td>
		                        <td>Jarak dari tempat tinggal kesekolah</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>17.</td>
		                        <td>Berkendaraan atau jalan kaki</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td valign="top">
                    <h3 style="font-weight:bold;">C. KETERANGAN KESEHATAN</h3>
                    <div style="padding-left:10px;">
                        <table cellpadding='0' cellspacing='0'>
                            <colgroup>
                                <col width="20px" />
                                <col width="170px"/>
                                <col width="20px"/>
                                <col />
                            </colgroup>
                            <tr>
		                        <td>18.</td>
		                        <td>Berat badan</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>  
                            <tr>
		                        <td>19.</td>
		                        <td>Tinggi badan</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>20.</td>
		                        <td>Golongan darah</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>21.</td>
		                        <td colspan="3">Penyakit yang pernah diderita (misalnya:TBC, Cacar, dan lain-lain)</td>
	                        </tr>
                            <tr>
                                <td></td>
                                <td colspan="3">
                                    <table border="1" cellpadding='0' cellspacing='0'>
                                        <colgroup>
                                            <col width="20px"/>
                                            <col width="120px"/>
                                            <col width="50px"/>
                                            <col width="50px"/>
                                            <col width="70"/>
                                            <col width="300px"/>
                                        </colgroup>
                                        <tr align="center">
                                            <td align="left">No.</td>
                                            <td>Jenis Penyakit</td>
                                            <td>Kelas</td>
                                            <td>Tahun</td>
                                            <td>Lama Sakit</td>
                                            <td>Keterangan</td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;</td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
		                        <td>22.</td>
		                        <td>Kelainan jasmani lainnya</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
                <td valign="top">
                    <h3 style="font-weight:bold;">D. KETERANGAN PENDIDIKAN SEBELUMNYA</h3>
                    <div style="padding-left:10px;">
                        <table cellpadding='0' cellspacing='0'>
                            <colgroup>
                                <col width="20px" />
                                <col width="170px"/>
                                <col width="20px"/>
                                <col />
                            </colgroup>
                            <tr>
		                        <td>23.</td>
		                        <td>Asal Sekolah</td>
		                        <td></td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>a. SMP/MTs</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>b. Tanggal dan No. STL</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>c. Tanggal dan No. Ijazah</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>d. Lamanya Belajar</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td>24.</td>
		                        <td>Pindahan</td>
		                        <td></td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>a. SMA</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>b. Taggal dan No. Surat Pindahan</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>c. Tanggal di terima</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                            <tr>
		                        <td></td>
		                        <td>d. Alasan Pindah</td>
		                        <td>:</td>
		                        <td></td>
	                        </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="trReportBody">
            <td valign="top">
                <h3 style="font-weight:bold;">E. KETERANGAN ORANG TUA</h3>
                <div id="divParent" runat="server">
                    <table border="1" cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="20px"/>
                            <col width="120px"/>
                            <col width="200px"/>
                            <col width="200px"/>
                        </colgroup>
                        <tr align="center">
                            <td align="left">No</td>
                            <td></td>
                            <td>Ayah</td>
                            <td>Ibu</td>
                        </tr>
                        <tr>
                            <td>25.</td>
                            <td>Nama</td>
                            <td>{FatherName}</td>
                            <td>{MotherName}</td>
                        </tr>
                        <tr>
                            <td>26.</td>
                            <td>Tempat, tanggal lahir</td>
                            <td>{FatherDOB}</td>
                            <td>{MotherDOB}</td>
                        </tr>
                        <tr>
                            <td>27.</td>
                            <td>Kewarganegaraan</td>
                            <td>{FatherNationality}</td>
                            <td>{MotherNationality}</td>
                        </tr>
                        <tr>
                            <td>28.</td>
                            <td>Ijazah tertinggi</td>
                            <td>{FatherEducationLevel}</td>
                            <td>{MotherEducationLevel}</td>
                        </tr>
                        <tr>
                            <td>29.</td>
                            <td>Pekerjaan</td>
                            <td>{FatherJob}</td>
                            <td>{MotherJob}</td>
                        </tr>
                        <tr>
                            <td>30.</td>
                            <td>Penghasilan/bulan</td>
                            <td>{FatherSalary}</td>
                            <td>{MotherSalary}</td>
                        </tr>
                        <tr>
                            <td>31.</td>
                            <td>Alamat</td>
                            <td>{FatherAddress}</td>
                            <td>{MotherAddress}</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr class="trReportBody">
            <td valign="top">
                <h3 style="font-weight:bold;">F. KETERANGAN WALI</h3>
                <div id="divWali" runat="server">
                    <table border="1" cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="20px"/>
                            <col width="120px"/>
                            <col width="200px"/>
                            <col width="200px"/>
                        </colgroup>
                        <tr align="center">
                            <td align="left">No</td>
                            <td>Wali</td>
                            <td>Laki-laki</td>
                            <td>Perempuan</td>
                        </tr>
                        <tr>
                            <td>32.</td>
                            <td>Nama</td>
                            <td>{WaliPName}</td>
                            <td>{WaliWName}</td>
                        </tr>
                        <tr>
                            <td>33.</td>
                            <td>Tempat, tanggal lahir</td>
                            <td>{WaliPDOB}</td>
                            <td>{WaliWDOB}</td>
                        </tr>
                        <tr>
                            <td>34.</td>
                            <td>Kewarganegaraan</td>
                            <td>{WaliPNationality}</td>
                            <td>{WaliWNationality}</td>
                        </tr>
                        <tr>
                            <td>35.</td>
                            <td>Ijazah tertinggi</td>
                            <td>{WaliPEducationLevel}</td>
                            <td>{WaliWEducationLevel}</td>
                        </tr>
                        <tr>
                            <td>36.</td>
                            <td>Pekerjaan</td>
                            <td>{WaliPJob}</td>
                            <td>{WaliWJob}</td>
                        </tr>
                        <tr>
                            <td>37.</td>
                            <td>Penghasilan/bulan</td>
                            <td>{WaliPSalary}</td>
                            <td>{WaliWSalary}</td>
                        </tr>
                        <tr>
                            <td>38.</td>
                            <td>Alamat</td>
                            <td>{WaliPAddress}</td>
                            <td>{WaliWAddress}</td>
                        </tr>
                        <tr>
                            <td>39.</td>
                            <td>Hubungan</td>
                            <td>{WaliPRelationship}</td>
                            <td>{WaliWRelationship}</td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr class="trReportBody">
            <td valign="top">
                <h3 style="font-weight:bold;">G. KETERANGAN INTELEGENSI DAN KEGEMARAN</h3>
                <div>
                    <table cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="20px;" />
                            <col width="170px" />
                            <col width="20px" />
                            <col />
                        </colgroup>
                        <tr>
		                    <td>40.</td>
		                    <td>Intelegasi (IQ)</td>
		                    <td>:</td>
		                    <td></td>
	                    </tr>
                        <tr>
		                    <td></td>
		                    <td>Berdasarkan tes tanggal-bulan-tahun</td>
		                    <td>:</td>
		                    <td></td>
	                    </tr>
                        <tr>
		                    <td>41.</td>
		                    <td colspan="3">Aspek kepribadian</td>
	                    </tr>
                        <tr>
		                    <td></td>
		                    <td colspan="3">
                                <table cellpadding="0" cellspacing="0" border="1">
                                    <tr>
                                        <td>No.</td>
                                        <td>Aspek yang dinilai</td>
                                        <td>Sangat Baik</td>
                                        <td>Baik</td>
                                        <td>Cukup</td>
                                        <td>Kurang</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptPersonality">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%# Container.ItemIndex + 1 %></td>
                                                <td><%#:Eval("SubjectName") %></td>
                                                <td runat="server" id="tdVeryGood"></td>
                                                <td runat="server" id="tdGood"></td>
                                                <td runat="server" id="tdEnough"></td>
                                                <td runat="server" id="tdPoor"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
	                    </tr>
                        <tr>
		                    <td>42.</td>
		                    <td colspan="3">Berkat khusus dan prestasi yang menonjol dalam bidang</td>
	                    </tr>
                        <tr>
		                    <td></td>
		                    <td>a. Kesenian</td>
		                    <td>:</td>
		                    <td></td>
	                    </tr>
                        <tr>
		                    <td></td>
		                    <td>b. Olah raga</td>
		                    <td>:</td>
		                    <td></td>
	                    </tr>
                        <tr>
		                    <td></td>
		                    <td>c. Kemasyarakatan/organisasi</td>
		                    <td>:</td>
		                    <td></td>
	                    </tr>
                        <tr>
		                    <td></td>
		                    <td>d. Karya tulis</td>
		                    <td>:</td>
		                    <td></td>
	                    </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr class="trReportBody">
            <td valign="top">
                <h3 style="font-weight:bold;">H. KETERANGAN KEHADIRAN</h3>
                <div>
                    <table>
                        <colgroup>
                            <col width="20px;" />
                            <col width="170px" />
                            <col width="20px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td>43.</td>
                            <td>Jumlah hari hadir tiap semester</td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <table border="1" cellpadding="0" cellspacing="0">
                                    <colgroup>
                                        <col width="120px" />
                                        <col width="30px"/>
                                        <col width="30px"/>
                                        <col width="30px"/>
                                        <col width="30px"/>
                                        <col width="30px"/>
                                        <col width="30px"/>
                                        <col width="30px"/>
                                        <col width="120px"/>
                                    </colgroup>
                                    <tr align="center">
                                        <td rowspan="2">Semester / Kelas</td>
                                        <td colspan="2">Hadir</td>
                                        <td colspan="5">Tidak hadir karena</td>
                                        <td rowspan="2">Jumlah hari belajar efektif</td>
                                    </tr>
                                    <tr align="center">
                                        <td>Jml</td>
                                        <td>%</td>
                                        <td>Sakit</td>
                                        <td>Ijin</td>
                                        <td>Alfa</td>
                                        <td>Jml</td>
                                        <td>%</td>
                                    </tr>
                                    <asp:Repeater runat="server" ID="rptAttendace" OnItemDataBound="rptAttendace_ItemDataBound">
                                        <ItemTemplate>
                                            <tr>
                                                <td><%#:Eval("PeriodSectionName") %></td>
                                                <td></td>
                                                <td></td>
                                                <td id="tdSakit" runat="server"></td>
                                                <td id="tdIzin" runat="server"></td>
                                                <td id="tdAlfa" runat="server"></td>
                                                <td id="tdJmlIzin" runat="server"></td>
                                                <td id="tdJmlIzinInPercentage" runat="server"></td>
                                                <td id="tdTotalDay" runat="server"></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr class="trReportBody">
            <td valign="top">
                <h3 style="font-weight:bold;">I. KETERANGAN PERKEMBANGAN</h3>
                <div>
                    <table cellpadding="0" cellspacing="0">
                        <colgroup>
                            <col width="20px;" />
                            <col width="170px" />
                            <col width="20px" />
                            <col />
                        </colgroup>
                        <tr>
                            <td>44.</td>
                            <td>Tahun masuk terdaftar</td>
                            <td>:</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>44.</td>
                            <td colspan="3">Prestasi</td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <table cellpadding="0" cellspacing="0" border="1">
                                    <tr align="center">
                                        <td>Tahun<br/>Pelajaran</td>
                                        <td>Kelas</td>
                                        <td>Smt</td>
                                        <td>Program</td>
                                        <td>Jumlah<br/>Mat. Pel.<br/>Tuntas</td>
                                        <td>Total<br/>mat. Pel.</td>
                                        <td>Naik/tdk. Naik<br/>Lulus/tdk. Lulus</td>
                                        <td>Keterangan</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>46.</td>
                            <td>Tahun meninggalkan sekolah</td>
                            <td>:</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>47.</td>
                            <td>Pindah sekolah ke</td>
                            <td>:</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>48.</td>
                            <td>Melanjutkan pendidikan ke</td>
                            <td>:</td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr class="trReportBody">
            <td valign="top">
                <h3 style="font-weight:bold;">J. PENERIMAAN BEA SISWA</h3>
                <div>
                    <table cellpadding="0" cellspacing="0" style="border:1px solid;">
                        <colgroup>
                            <col width="70px"/>
                            <col width="10px" />
                            <col width="200px"/>
                            <col width="20px" />
                            <col width="70px" />
                            <col width="10px" />
                            <col width="200px" />
                        </colgroup>
                        <tr>
                            <td>Tahun</td>
                            <td>:</td>
                            <td></td>
                            <td>&nbsp;</td>
                            <td>Dari</td>
                            <td>:</td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
            <tr class="trReportBody">
            <td style="height:151.5px" valign="top">
                <center><h3 style="font-weight:bold;">PRESTASI BELAJAR</h3></center>
                <div>
                    <table width="100%" class="tblRapor" cellpadding="0" cellspacing="0">
                        <tr>
                            <td align="center" class="lblHeader">Komponen</td>
                            <td rowspan="3" colspan="2" align="center" class="lblHeader">Nilai SMP</td>
                            <td colspan="8" align="center" class="lblHeader" id="tdTahunAjaran" runat="server">{TahunAjaranMulai}</td>
                            <td colspan="8" align="center" class="lblHeader" id="tdTahunAjaran1" runat="server">{TahunAjaranMulai1}</td>
                        </tr>
                        <tr>
                            <td align="center" class="lblHeader">Kelas</td>
                            <td colspan="8" align="center" class="lblHeader">{Grade1}/{Kelas1}</td>
                            <td colspan="8" align="center" class="lblHeader">{Grade2}/{Kelas2}</td>
                        </tr>
                        <tr>
                            <td align="center" class="lblHeader">Nilai</td>
                            <td align="center" class="lblHeader">KKM</td>
                            <td colspan="3" align="center" class="lblHeader">SMT-1</td>
                            <td align="center" class="lblHeader">KKM</td>
                            <td colspan="3" align="center" class="lblHeader">SMT-2</td>
                            <td align="center" class="lblHeader">KKM</td>
                            <td colspan="3" align="center" class="lblHeader">SMT-1</td>
                            <td align="center" class="lblHeader">KKM</td>
                            <td colspan="3" align="center" class="lblHeader">SMT-2</td>
                        </tr>
                        <tr>
                            <td align="center" class="lblHeader">Aspek</td>
                            <td align="center" class="lblHeader">Stl</td>
                            <td align="center" class="lblHeader">UP<br/>MP</td>
                            <td align="center" class="lblHeader"></td>
                            <td align="center" class="lblHeader">K</td>
                            <td align="center" class="lblHeader">P</td>
                            <td align="center" class="lblHeader">A</td>
                            <td align="center" class="lblHeader"></td>
                            <td align="center" class="lblHeader">K</td>
                            <td align="center" class="lblHeader">P</td>
                            <td align="center" class="lblHeader">A</td>
                            <td align="center" class="lblHeader"></td>
                            <td align="center" class="lblHeader">K</td>
                            <td align="center" class="lblHeader">P</td>
                            <td align="center" class="lblHeader">A</td>
                            <td align="center" class="lblHeader"></td>
                            <td align="center" class="lblHeader">K</td>
                            <td align="center" class="lblHeader">P</td>
                            <td align="center" class="lblHeader">A</td>
                        </tr>
                        <asp:Repeater runat="server" ID="rptSubject" OnItemDataBound="rptSubject_ItemDataBound">
                            <ItemTemplate>
                                <tr>
                                    <td><%#:Eval("SubjectName") %></td>
                                    <td></td>
                                    <td></td>
                                    <asp:Repeater runat="server" ID="rptSbjPerPeriod" OnItemDataBound="rptSbjPerPeriod_ItemDataBound">
                                        <ItemTemplate>
                                            <td align="right" runat="server" id="tdPassingGrade"></td>
                                            <td align="right" runat="server" id="tdTheory"></td>
                                            <td align="right" runat="server" id="tdPractice"></td>
                                            <td align="right" runat="server" id="tdAffective"></td>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div>
            </td>
        </tr>
        </tbody>
    </table>
</div>

<div id="divPageFooter" runat="server">
    <style type="text/css">
        .pageFooter         { border-top: 0px solid; font-size: 8pt !important; margin-bottom: 50px; }
        .pageFooter *       { font-size: 8pt !important; }
        .letterFooter       { width:150px; text-align:center}
    </style>
    
</div>

