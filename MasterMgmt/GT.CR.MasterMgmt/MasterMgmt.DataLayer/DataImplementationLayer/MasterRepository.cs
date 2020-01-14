
///-----------------------------------------------------------------
///   Namespace:    MasterMgmt.DataLayer.DataImplementationLayer
///   Class:         MasterRepository
///   Description:    Data Layer for Master data
///   Author:        Keshav M                   Date: 21/6/2017
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------


using MasterMgmt.CommonLayer.Models.Entities;
using MasterMgmt.DataLayer.DataInterfaceLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MasterMgmt.Exceptions;
using Microsoft.EntityFrameworkCore;
using MasterMgmt.CommonLayer.Models.DTO;
using System.Data.SqlClient;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Internal;
using System.Data;
using System.Reflection;

namespace MasterMgmt.DataLayer.DataImplementationLayer
{
    public class MasterRepository : IMasterRepository
    {
        private readonly MasterContext _masterContext = null;
        private readonly IMapper Mapper = null;
        SqlConnection connection;
        readonly IConfiguration configuration;
        public MasterRepository(MasterContext masterContext, IMapper mapper, IConfiguration _Configuration)
        {
            _masterContext = masterContext;
            Mapper = mapper;
            configuration = _Configuration;
        }

      
        /// <summary>
        /// Returns list of all checklists by Account 
        /// </summary>
        /// <returns>List of Checklist</returns>
        public List<MasterChecklistDTO> GetAllChecklistsBySubAreas(List<string> SubAreaCode)
        {
            try
            {

                var parameter = new SqlParameter("@SubAreaCodes", SqlDbType.Structured); // defining the parameter and the type 
                parameter.Value = ToDataTable(SubAreaCode); // passing value to the data model
                parameter.TypeName = "[dbo].[SubAreaCodes]";
                var checklists = _masterContext.MasterChecklistDTO.FromSql("Sp_GetAllChecklistBySubAreas @SubAreaCodes", parameter).ToList();
                return checklists;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Returns a check
        /// </summary>
        /// <returns>Check</returns>
        public Check GetCheckByCheckCode(string CheckCode)
        {
            try
            {
                return _masterContext.Check.Where(x => x.CheckCode == CheckCode).FirstOrDefault(); // Returns a check
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Returns a checklist
        /// </summary>
        /// <returns>Checklist</returns>
        public MasterChecklistDTO GetChecklistByChecklistCode(string ChecklistCode, string Subareacode)
        {
            try
            {
                var CheckList = _masterContext.Checklist.Where(c => c.ChecklistCode == ChecklistCode).FirstOrDefault();
                CheckList.Checks = _masterContext.AreaChecks.Where(a => a.ChecklistID == CheckList.ChecklistID && a.SubAreaCode == Subareacode).Select(a => a.Check).Distinct().ToList();
                CheckList.MaxScore = CheckList.Checks.Sum(c => c.Score);
                CheckList.TotalNoOfChecks = CheckList.Checks.Count();
                return Mapper.Map<Checklist, MasterChecklistDTO>(CheckList);
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Returns list of all checks
        /// </summary>
        /// <returns>List of Checks</returns>
        public List<Check> GetAllChecks()
        {
            try
            {
                return _masterContext.Check.OrderBy(x => x.CheckTitle.Trim()).ToList(); // Returns list of all checks
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Saves list of checklist
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns>True on success</returns>
        public object CreateNewChecklist(List<Checklist> checklists)
        {
            try
            {
                List<string> checklistCodes = new List<string>();
                foreach (Checklist cl in checklists)
                {
                    var dBClId = _masterContext.Checklist
                        .Where(c => c.ChecklistName == cl.ChecklistName && c.ChecklistType == cl.ChecklistType && c.ChecklistID != cl.ChecklistID)
                                                         .Select(c => c.ChecklistID)
                                                         .FirstOrDefault();
                    //  Edited & Id different for same name(Duplicate CL) (or) Found Duplicate while creating
                    if ((cl.ChecklistID != 0 && dBClId != cl.ChecklistID) || (cl.ChecklistID == 0 && dBClId != 0))
                        return checklistCodes;
                    _masterContext.Entry(cl).State = cl.ChecklistID == 0 ? EntityState.Added : EntityState.Modified;

                    cl.Medias?.ForEach(m =>
                    {
                        _masterContext.Entry(m).State = m.MediaID == 0 ? EntityState.Added : EntityState.Modified;
                    });

                    cl.AreaChecks.ForEach(ac =>
                    {
                        ac.ChecklistID = cl.ChecklistID;
                        ac.AreaChecksID = _masterContext
                                                        .AreaChecks
                                                        .Where(a => a.AreaChecksID == ac.AreaChecksID && a.CheckID == ac.CheckID && a.ChecklistID == ac.ChecklistID)
                                                        .Select(a => a.AreaChecksID)
                                                        .FirstOrDefault();
                        _masterContext.Entry(ac).State = ac.AreaChecksID == 0 ? EntityState.Added : EntityState.Modified;
                    });
                    _masterContext.SaveChanges();
                    _masterContext.Entry(cl).Reload();
                    checklistCodes.Add(cl.ChecklistCode);
                }
                return checklistCodes;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }
        /// <summary>
        /// Saves list of checklist
        /// </summary>
        /// <param name="checklist"></param>
        /// <returns>True on success</returns>
        public object EditChecklist(Checklist checklist)
        {
            try
            {
                List<string> checklistCodes = new List<string>();

                var dBClId = _masterContext.Checklist
                        .Where(c => c.ChecklistName == checklist.ChecklistName && c.ChecklistType == checklist.ChecklistType && c.ChecklistID != checklist.ChecklistID)
                                                         .Select(c => c.ChecklistID)
                                                         .FirstOrDefault();
                //  Edited & Id different for same name(Duplicate CL) (or) Found Duplicate while creating
                if ((checklist.ChecklistID != 0 && dBClId != checklist.ChecklistID && dBClId != 0) || (checklist.ChecklistID == 0 && dBClId != 0))
                {
                    //string checklistCode = _masterContext.Checklist.Where(c => c.ChecklistID == dBClId).Select(c => c.ChecklistCode).FirstOrDefault().ToString();
                    //checklistCodes.Add(checklistCode);
                    return checklistCodes;
                }

                _masterContext.Entry(checklist).State = checklist.ChecklistID == 0 ? EntityState.Added : EntityState.Modified;

                checklist.Medias?.ForEach(m =>
                    {
                        _masterContext.Entry(m).State = m.MediaID == 0 ? EntityState.Added : EntityState.Modified;
                    });

                List<AreaChecks> oldAreaChecks = new List<AreaChecks>();
                oldAreaChecks = _masterContext.AreaChecks.Where(a => a.ChecklistID == checklist.ChecklistID).ToList();

                foreach (AreaChecks oldAreaCheck in oldAreaChecks)
                {
                    _masterContext.AreaChecks.Remove(oldAreaCheck);
                    _masterContext.SaveChanges();
                    //_masterContext.Entry(oldAreaCheck).State = EntityState.Deleted;
                }

                checklist.AreaChecks.ForEach(ac =>
                    {
                        ac.ChecklistID = checklist.ChecklistID;
                        ac.AreaChecksID = _masterContext
                                                        .AreaChecks
                                                        .Where(a => a.AreaChecksID == ac.AreaChecksID && a.CheckID == ac.CheckID && a.ChecklistID == ac.ChecklistID)
                                                        .Select(a => a.AreaChecksID)
                                                        .FirstOrDefault();
                        _masterContext.Entry(ac).State = ac.AreaChecksID == 0 ? EntityState.Added : EntityState.Modified;
                    });
                //_masterContext.Entry(checklist).Property(x => x.CreatedBy).IsModified = false;
                //_masterContext.Entry(checklist).Property(x => x.CreatedDateTime).IsModified = false;                    
                _masterContext.SaveChanges();
                _masterContext.Entry(checklist).Reload();
                checklistCodes.Add(checklist.ChecklistCode);

                return checklistCodes;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Save a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        public bool SaveChecks(List<Check> checks)
        {
            try
            {
                foreach (var check in checks)
                {
                    _masterContext.Entry(check).State = check.CheckID == 0 ? EntityState.Added : EntityState.Modified; // Checking the state of the check
                }

                _masterContext.SaveChanges(); // Saving all changes

                return true;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Checks if a check title already exits in master data
        /// </summary>
        /// <param name="CheckTitle"></param>
        /// <param name="CheckID"></param>
        /// <returns></returns>
        public bool ValidateDuplicateCheckTitle(string CheckTitle, int CheckID)
        {
            try
            {
                if (CheckID == 0)
                {
                    return _masterContext.Check.Any(ch => ch.CheckTitle.Trim() == CheckTitle.Trim());
                }
                else
                {
                    return _masterContext.Check.Any(ch => ch.CheckTitle.Trim() == CheckTitle.Trim() && ch.CheckID != CheckID);
                }
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }


        /// <summary>
        /// Checks if a checklist title already exits in master data
        /// </summary>
        /// <param name="ChecklistName"></param>
        /// <param name="ChecklistID"></param>
        /// <returns></returns>
        public bool ValidateDuplicateChecklistTitle(string SubAreaCode, string ChecklistName, int ChecklistID)
        {
            try
            {
                var ChecklistIdList = _masterContext.AreaChecks.Where(ac => ac.SubAreaCode == SubAreaCode).Select(ac => ac.ChecklistID).Distinct().ToList();

                foreach (var checklist in ChecklistIdList)
                {

                    if (_masterContext.Checklist.Any(ch => ch.ChecklistName.Trim() == ChecklistName.Trim() && ch.ChecklistID != ChecklistID && ch.ChecklistID == checklist))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Check if a checklist is already exits in master data by Name,Checks and SubAreaCode
        /// </summary>
        /// <param name="SubAreaCode"></param>
        /// <param name="SelectedChecks"></param>
        /// <param name="ChecklistID"></param>
        /// <returns></returns>
        public bool ValidateDuplicateChecklistChecks(string SubAreaCode, int[] SelectedChecks, int ChecklistID)
        {
            try
            {

                var ChecklistIdList = _masterContext.AreaChecks.Where(ac => ac.SubAreaCode == SubAreaCode).Select(ac => ac.ChecklistID).Distinct().ToList();

                foreach (var checklistID in ChecklistIdList)
                {
                    int[] TotalChecks = _masterContext.AreaChecks.Where(ac => ac.ChecklistID == checklistID && ac.ChecklistID != ChecklistID && ac.ChecklistID == checklistID)
                                      .Select(ac => ac.CheckID).Distinct().ToArray();
                    Array.Sort(TotalChecks);
                    Array.Sort(SelectedChecks);

                    if (Enumerable.SequenceEqual(TotalChecks, SelectedChecks))
                    {
                        return true;
                    }

                }

                return false;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Save a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        public object CreateNewCheck(Check Check)
        {
            try
            {
                if (_masterContext.Check.Any(c => c.CheckTitle.Trim() == Check.CheckTitle.Trim() && c.Score == Check.Score && c.CheckAnswer == Check.CheckAnswer))
                    return "";

                _masterContext.Entry(Check).State = EntityState.Added;
                _masterContext.SaveChanges();
                _masterContext.Entry(Check).Reload();

                return _masterContext.Check.FirstOrDefault(c => c.CheckTitle.Trim() == Check.CheckTitle.Trim()).CheckCode;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Edit a check
        /// </summary>
        /// <param name="check"></param>
        /// <returns>True on success</returns>
        public object EditCheck(Check Check)
        {
            try
            {
                if (_masterContext.Check.Any(c => c.CheckTitle.Trim() == Check.CheckTitle.Trim() && c.CheckID != Check.CheckID))
                    return false;

                _masterContext.Entry(Check).State = EntityState.Modified;
                _masterContext.Entry(Check).Property(x => x.CreatedBy).IsModified = false;
                _masterContext.Entry(Check).Property(x => x.CreatedDateTime).IsModified = false;
                _masterContext.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Get All Checklist With Checks By ChecklistID
        /// </summary>
        /// <param name="ChecklistID"></param>
        /// <returns>Checklist</returns>
        public Checklist GetChecklistWithChecksByChecklistID(int ChecklistID)
        {
            try
            {
                var checkList = _masterContext.Checklist.First(c => c.ChecklistID == ChecklistID);
                checkList.Checks = _masterContext.AreaChecks.Where(a => a.ChecklistID == ChecklistID).Select(a => a.Check).ToList();
                checkList.MaxScore = checkList.Checks.Sum(c => c.Score);
                return checkList;

            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        /// <summary>
        /// Get all checklists for particular area
        /// </summary>
        /// <param name="CompanyCode"></param>
        /// <param name="LocationCode"></param>
        /// <param name="DepartmentCode"></param>
        /// <param name="AreaCode"></param>
        /// <param name="SubAreaCode"></param>
        /// <returns>List of checklist for that area</returns>
        public List<ChecklistDTO> GetChecklistForArea(string SubAreaCode)
        {
            try
            {
                return Mapper.Map<List<Checklist>, List<ChecklistDTO>>(_masterContext.AreaChecks.Where(ac => ac.SubAreaCode == SubAreaCode).Select(ac => ac.Checklist).Distinct().ToList());
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        public List<AuditInfo> GetAuditInfo()
        {
            return _masterContext.AuditInfo.OrderByDescending(x => x.CreatedDateTime).ToList();
        }
        public List<AuditInfo> GetAuditInfo(DateTime StartDateTime, DateTime EndDateTime)
        {
            return _masterContext.AuditInfo.Where(a => a.AuditScheduledStartDateTime >= StartDateTime && a.AuditScheduledStartDateTime <= EndDateTime).OrderByDescending(x => x.CreatedDateTime).ToList();
        }


        public List<ChecklistDTO> GetChecklistsByAuditID(int AuditID)
        {
            try
            {
                List<ChecklistDTO> checklistDTOs = new List<ChecklistDTO>();
                var asdf = _masterContext.Checklist.Join(_masterContext.AreaChecks,
                                    cl => cl.ChecklistID,
                                    ac => ac.ChecklistID,
                                    (cl, ac) => new { ac.AreaChecksID, ac.SubAreaCode, cl.ChecklistID, ac.CheckID }).Join(_masterContext.AuditCheck.Where(a => a.AuditInfoID == AuditID),
                                    acl => acl.AreaChecksID,
                                    ac => ac.AreaChecksID,
                                    (acl, ac) => new { ac.AuditChecklistInfoID, acl.SubAreaCode, acl.ChecklistID, acl.CheckID }).Join(_masterContext.AuditChecklistInfo,
                                    acl => acl.AuditChecklistInfoID,
                                    acli => acli.AuditChecklistInfoID,
                                    (acl, acli) => new { acli.AuditChecklistInfoID, acli.SubAreaCode, acl.ChecklistID, acl.CheckID })
                                    .Join(_masterContext.Check,
                                    acl => acl.CheckID,
                                    c => c.CheckID,
                                    (ac, c) => new { c, ac }).GroupBy(c => new { c.ac.ChecklistID, c.ac.SubAreaCode })
                                     .Select(c => c.Select(v => new { MaxScore = c.Sum(s => s.c.Score), CheckCount = c.Count(), v.ac.ChecklistID, v.ac.AuditChecklistInfoID }))
                                    .Join(_masterContext.AuditChecklistInfo,
                                    c => c.Select(s => s.AuditChecklistInfoID).First(),
                                    acli => acli.AuditChecklistInfoID,
                                    (c, acli) => new { summary = c.First(), acli })
                                     .Join(_masterContext.Checklist,
                                    c => c.summary.ChecklistID,
                                    cl => cl.ChecklistID,
                                    (c, cl) => new { c, cl })
                                    .ToList();

                asdf.ForEach(cl =>
                {
                    var _cl = Mapper.Map<Checklist, ChecklistDTO>(cl.cl);
                    _cl.MaxScore = double.Parse(cl.c.summary.MaxScore.ToString());
                    _cl.TotalNoOfChecks = double.Parse(cl.c.summary.CheckCount.ToString());
                    _cl.AreaName = cl.c.acli.AreaName;
                    _cl.SubAreaCode = cl.c.acli.SubAreaCode;
                    _cl.SubAreaName = cl.c.acli.SubAreaName;
                    _cl.ChecklistScheduledStartDateTime = cl.c.acli.ChecklistScheduledStartDateTime;
                    _cl.ChecklistScheduledEndDateTime = cl.c.acli.ChecklistScheduledEndDateTime;
                    checklistDTOs.Add(_cl);
                });
                return checklistDTOs.Where(cl => cl.MaxScore != 0).ToList();
            }
            catch (Exception e)
            {

                throw new DataLayerException(" Error while Get Checklists By AuditID", e);
            }

        }

        public List<Check> GetAllChecksByChecklistID(int ChecklistID, string SubAreaCode)
        {
            try
            {
                return _masterContext.AreaChecks.Where(c => c.ChecklistID == ChecklistID && c.SubAreaCode == SubAreaCode).Select(a => a.Check).ToList(); // Returns list of all checks
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }
        public List<Check> GetAllChecksByChecklistID(int ChecklistID)
        {
            try
            {
                return _masterContext.AreaChecks.Where(c => c.ChecklistID == ChecklistID).Select(a => a.Check).ToList(); // Returns list of all checks
            }
            catch (Exception e)
            {
                throw new DataLayerException("Data Layer Exception", e);
            }
        }

        public CountDto GetCountForPublishedAudits(string Fbo)
        {
            return new CountDto()
            {
                PublishedCount = _masterContext.AuditInfo.Where(p => p.AuditFBO == Fbo).Count()
            };
        }

        public List<string> SaveAuditInfo(List<AuditInfoDto> auditInfoes)
        {
            Stack<string> _returnvalue = new Stack<string>();
            List<string> returnvalue = new List<string>();
            try
            {
                if (auditInfoes != null && auditInfoes.Any(a => a.AuditChecklistDto.Count > 0))
                {
                    foreach (var auditInfo in auditInfoes)
                    {
                        //get AuditId by Saving Audit
                        AuditInfo _auditInfo = Mapper.Map<AuditInfoDto, AuditInfo>(auditInfo);
                        _masterContext.Add(_auditInfo);
                        _masterContext.SaveChanges();
                        int AuditId = _auditInfo.AuditInfoID;
                        foreach (var checklist in auditInfo.AuditChecklistDto)
                        {
                            //get checkListInfoId by Saving Audit

                            AuditChecklistInfo _auditChecklistInfo = Mapper.Map<AuditChecklistDto, AuditChecklistInfo>(checklist);

                            _masterContext.Add(_auditChecklistInfo);
                            _masterContext.SaveChanges();

                            int checkListInfoId = _auditChecklistInfo.AuditChecklistInfoID;
                            var areaChecks = _masterContext.AreaChecks.Where(area => area.ChecklistID == checklist.ChecklistID && area.SubAreaCode == checklist.SubAreaCode).ToList();
                            foreach (var areaCheck in areaChecks)
                            {

                                _masterContext.Add(new AuditCheck()
                                {
                                    AuditInfoID = AuditId,
                                    AreaChecksID = areaCheck.AreaChecksID,
                                    AuditChecklistInfoID = checkListInfoId
                                });
                            }
                            _masterContext.SaveChanges();
                        }
                    }
                    connection = new SqlConnection(configuration.GetConnectionString("DefaultConnectionString"));
                    _returnvalue = new Stack<string>(connection.Query<string>("Exec Sp_UpdateAuditCode").ToList());

                }
                for (int i = 0; i < auditInfoes?.Count; i++)
                {
                    if (auditInfoes[i].AuditChecklistDto.Count == 0 || _returnvalue.Count == 0)
                        returnvalue.Add(null);
                    else
                        returnvalue.Add(_returnvalue.Pop());
                }
                return returnvalue;
            }
            catch (Exception e)
            {
                throw new DataLayerException(" Error while Saving Audit Info", e);
            }
            finally
            {
                connection.Close();
            }
        }



        //public List<string> UpdatePublishedAudits(List<AuditInfoDto> auditInfoes)
        //{
        //    foreach (var ai in auditInfoes)
        //    {
        //        var auditinfo = Mapper.Map<AuditInfoDto, AuditInfo>(ai);

        //        _masterContext.Entry(auditinfo).State = EntityState.Modified;
        //        if (ai.AuditChecklistDto != null && auditInfoes.Any(a => a.AuditChecklistDto.Count > 0))
        //        {
        //            _masterContext.AuditCheck.RemoveRange(_masterContext.AuditCheck.Where(ac => ac.AuditInfoID == ai.AuditInfoID));
        //            _masterContext.SaveChanges(); 
        //            foreach (var acl in ai.AuditChecklistDto)
        //            {
        //                    _masterContext.AuditChecklistInfo.Add(acl);

        //                var areaChecks = _masterContext.AreaChecks.Where(area => area.ChecklistID == acl.ChecklistID && area.SubAreaCode == acl.SubAreaCode).ToList();

        //                foreach (var aac in areaChecks)
        //                {
        //                    var b = new AuditCheck();
        //                    b.AuditInfo = auditinfo;
        //                    b.AreaChecks = aac;
        //                    b.AuditChecklistInfo = Mapper.Map<AuditChecklistDto, AuditChecklistInfo>(acl);

        //                    _masterContext.AuditCheck.Add(b);
        //                }
        //                    _masterContext.SaveChanges(); 
        //            }
        //        }
        //    }
        //    return auditInfoes.Select(a => a.AuditCode).ToList();
        //}

        public List<string> UpdatePublishedAudits(List<AuditInfoDto> auditInfoes)
        {
            string _returnvalue = null;
            List<string> returnvalue = new List<string>();
            try
            {
                if (auditInfoes != null && auditInfoes.Any(a => a.AuditChecklistDto.Count > 0))
                {
                    foreach (var auditInfo in auditInfoes)
                    {
                        //get AuditId by Saving Audit
                        AuditInfo _auditInfo = Mapper.Map<AuditInfoDto, AuditInfo>(auditInfo);
                        _masterContext.Entry(_auditInfo).State = EntityState.Modified;
                        _masterContext.SaveChanges();
                        int AuditId = _auditInfo.AuditInfoID;
                        _masterContext.AuditCheck.RemoveRange(_masterContext.AuditCheck.Where(ac => ac.AuditInfoID == AuditId));
                        _masterContext.SaveChanges();
                        foreach (var checklist in auditInfo.AuditChecklistDto)
                        {
                            AuditChecklistInfo _auditChecklistInfo = Mapper.Map<AuditChecklistDto, AuditChecklistInfo>(checklist);
                            if (_auditChecklistInfo.AuditChecklistInfoID == 0)
                            {
                                _masterContext.Add(_auditChecklistInfo);
                                _masterContext.SaveChanges();

                                int checkListInfoId = _auditChecklistInfo.AuditChecklistInfoID;
                                var areaChecks = _masterContext.AreaChecks.Where(area => area.ChecklistID == checklist.ChecklistID && area.SubAreaCode == checklist.SubAreaCode).ToList();
                                foreach (var areaCheck in areaChecks)
                                {
                                    _masterContext.Add(new AuditCheck()
                                    {
                                        AuditInfoID = AuditId,
                                        AreaChecksID = areaCheck.AreaChecksID,
                                        AuditChecklistInfoID = checkListInfoId
                                    });
                                }
                                _masterContext.SaveChanges();
                            }

                        }
                        _returnvalue = _auditInfo.AuditCode;
                    }
                    returnvalue.Add(_returnvalue);
                }
                return returnvalue;
            }
            catch (Exception e)
            {
                throw new DataLayerException("Error while updating the published audits", e);
            }

        }



        #region Data Conversion for Data Table
        /// <summary>
        /// List Conversion to DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        private DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            if (items != null)
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        private DataTable ToDataTable(List<string> items)
        {
            DataTable dataTable = new DataTable("List");

            dataTable.Columns.Add("Value", typeof(string));

            foreach (string item in items)
            {
                dataTable.Rows.Add(item);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        #endregion
    }
}
