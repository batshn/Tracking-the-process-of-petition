using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProcessOfApplicaion.Models
{

    
    public class ProcessInfo
    {
        [JsonProperty("id")]
        public string mID { get; set; }

        [JsonProperty("state")]
        public string mState { get; set; }

        [JsonProperty("stateHint")]
        public string mStateHint { get; set; }

        [JsonProperty("name")]
        public string mName { get; set; }

        [JsonProperty("trans_mode")]
        public string mTrans_mode { get; set; }

        [JsonProperty("sender_name")]
        public string mSender_name { get; set; }

        [JsonProperty("for_user_name")]
        public string mFor_user_name { get; set; }

        [JsonProperty("for_user_depName")]
        public string mFor_user_depName { get; set; }

        [JsonProperty("own_date")]
        public string mOwn_date { get; set; }

        [JsonProperty("answer_date")]
        public string mAnswer_date { get; set; }

        [JsonProperty("answered_date")]
        public string mAnswered_date { get; set; }

        [JsonProperty("answered_type")]
        public string mAnswered_type { get; set; }

        [JsonProperty("answered_user_name")]
        public string mAnswered_user_name { get; set; }

        [JsonProperty("answer_snoozed_date")]
        public string mAnswer_snoozed_date { get; set; }

        [JsonProperty("close_note")]
        public string mClose_note { get; set; }

        [JsonProperty("snooze_reason")]
        public string mSnooze_reason { get; set; }


        [JsonProperty("holder_name")]
        public string mHolder_name { get; set; }

        [JsonProperty("holder_app")]
        public string mHolder_app { get; set; }

        [JsonProperty("holder_dep")]
        public string mHolder_dep { get; set; }

        [JsonProperty("date")]
        public string mDate { get; set; }

        [JsonProperty("shifts")]
        public List<Shift> shifts
        {
            get; set;
        }
    }

    public  class Shift
    {
        [JsonProperty("id")]
        public string mId { get; set; }

        [JsonProperty("receiver_com_name")]
        public string mReceiver_com_name { get; set; }

        [JsonProperty("is_start")]
        public string mIs_start { get; set; }

        [JsonProperty("receiver_name")]
        public string mReceiver_name { get; set; }

        [JsonProperty("receiver_dep")]
        public string mReceiver_dep { get; set; }

        [JsonProperty("receiver_app")]
        public string mReceiver_app { get; set; }

        [JsonProperty("receiver_work_phone")]
        public string mReceiver_work_phone { get; set; }

        [JsonProperty("receiver_phone")]
        public string mReceiver_phone { get; set; }

        [JsonProperty("receiver_mail")]
        public string mReceiver_mail { get; set; }

        [JsonProperty("rule")]
        public string mRule { get; set; }

        [JsonProperty("shift_date")]
        public string mShift_date { get; set; }

        [JsonProperty("next_user")]
        public string mNext_user { get; set; }

        [JsonProperty("next_dep")]
        public string mNext_dep { get; set; }

        [JsonProperty("next_app")]
        public string mNext_app { get; set; }

        [JsonProperty("next_work_phone")]
        public string mNext_work_phone { get; set; }

        [JsonProperty("next_phone")]
        public string mNext_phone { get; set; }

        [JsonProperty("next_mail")]
        public string mNext_mail { get; set; }

        [JsonProperty("own_complain")]
        public string mOwn_complain { get; set; }

        [JsonProperty("isClosed")]
        public string mIsClosed { get; set; }

        [JsonProperty("typeClose")]
        public string mTypeClose { get; set; }

        [JsonProperty("noteClose")]
        public string mNoteClose { get; set; }

        [JsonProperty("dateClose")]
        public string mDateClose { get; set; }

    }
}