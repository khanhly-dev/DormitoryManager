using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dormitory.Domain.Shared.Constant
{
    public class DataConfigConstant
    {
        public static int studentTenantId = 0;
        public static int adminTenantId = 1;

        public static int female = 0;
        public static int male = 1;

        public static int contractConfirmStatusPending = 0;
        public static int contractConfirmStatusReject = 1;
        public static int contractConfirmStatusApprove = 2;

        public static int contractCompletedStatusCancel = 0;
        public static int contractCompletedStatusOk = 1;

        public static bool extendContract = true;
        public static bool nonExtendContract = false;

        public static int ContractService = 1;
        public static int RoomService = 0;
    }
}
