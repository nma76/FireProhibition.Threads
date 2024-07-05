using FireProhibition.Lib.Model;

namespace FireProhibition.Threads.App.Interface
{
    internal interface IThreadsPost
    {
        string CreateTextPost(List<FireProhibitionStatus> fireProhibitions);
        string CreateTextPost(List<FireRiskStatus> riskStatuses);
    }
}
