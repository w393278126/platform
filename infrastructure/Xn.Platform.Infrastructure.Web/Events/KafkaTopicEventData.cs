using  Xn.Platform.Infrastructure.Web.Kafka;

namespace Xn.Platform.Infrastructure.Web.Events
{
    public enum KafkaTopicType
    {
        NotSet,
        Live,
        LivePlayerLog,
        Mb,
        UpLoadMediaLog,
        /// <summary>
        /// ���롢�뿪����
        /// </summary>
        JoinLeaveRoom,
        Gift,
        SearchSyncIndex,
        /// <summary>
        /// ��Ϊ��֤����
        /// </summary>
        CertifiedHost,
        /// <summary>
        /// �û��ȼ�����
        /// </summary>
        UserNewGrade,
        /// <summary>
        /// �����ȼ�����
        /// </summary>
        HostGrade,
        /// <summary>
        /// ��ע����
        /// </summary>
        Subscribe,
        /// <summary>
        /// �������
        /// </summary>
        WeekStar,
        /// <summary>
        /// ��������
        /// </summary>
        RoomCreate,
        /// <summary>
        /// ��ע
        /// </summary>
        Follow,
        /// <summary>
        /// ��������
        /// </summary>
        MissionEvent,
        /// <summary>
        /// ��������(ֱ��ʱ��)
        /// </summary>
        MissionEvent2,
        /// <summary>
        /// ��������(��ע����)
        /// </summary>
        MissionEvent3,
        /// <summary>
        /// ��������(�û�����)
        /// </summary>
        MissionEvent4,
        /// <summary>
        /// ������������ʱ��
        /// </summary>
        MissionEventOnline,
        /// <summary>
        /// �ڲ���ֵ
        /// </summary>
        BlackRecharge,
        /// <summary>
        /// �ڲ�ͷ��
        /// </summary>
        BlackAvatar,
        /// <summary>
        /// ������������
        /// </summary>
        LZAWSC,
        /// <summary>
        /// ������н����
        /// </summary>
        LZAWSB,
        /// <summary>
        /// ������ļ���
        /// </summary>
        LZARCC,
        /// <summary>
        /// �����Ӽ���
        /// </summary>
        LZAWSD,
        MbChat,
        /// <summary>
        /// ��ȯ�һ�
        /// </summary>
        LZAPPCouponExchange,
        /// <summary>
        /// ���������¼�
        /// </summary>
        Family,
        /// <summary>
        /// ����
        /// </summary>
        BlackScreen,
        /// <summary>
        /// �������VIP
        /// </summary>
        SystemNotify4UserProps,
        /// <summary>
        /// ������ͣ
        /// </summary>
        BatchClosure,
        /// <summary>
        /// ����Ʒ
        /// </summary>
        SendAward,
        /// <summary>
        /// ����Ʒ
        /// </summary>
        SendAward2,
        /// <summary>
        /// PPTV�˻��ϲ�
        /// </summary>
        PPTVAccountMerge,
        /// <summary>
        /// �˻�ע��
        /// </summary>
        AccountRegister,
        /// <summary>
        /// ��״̬������
        /// </summary>
        StreamStatus,
        /// <summary>
        /// ��ļ���ָ��
        /// </summary>
        ApplyRoom,
        /// <summary>
        /// ��¼����
        /// </summary>
        RecordConsume,
        /// <summary>
        /// �����Ǯ
        /// </summary>
        MillionaireAnswer,
        /// <summary>
        /// ��������н����
        /// </summary>
        NewLZAWSB,
        /// <summary>
        /// ����������topic
        /// </summary>
        WFC,
        /// <summary>
        /// �µ�н����
        /// </summary>
        LZAWSBN,
        RedEnvelope,
        /// <summary>
        /// �����¼������
        /// </summary>
        ItemEventMonitor,
        ConsumeGift,
        /// <summary>
        /// ����topic
        /// </summary>
        Settlement,
    }

    public class KafkaTopicEventData : EventData
    {
        public KafkaTopicType TopicType { get; set; }

        public string Key { get; set; }

        public object Data { get; set; }

        public Compression Codec { get; set; } = Compression.None;
    }
}