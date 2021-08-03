using SaudeMental.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SaudeMental.Api.Services
{
    public interface IReportService
    {
        public byte[] GeneratePdfReport(List<FormInfo> formInfos, UserInfo userInfo);
    }
}
