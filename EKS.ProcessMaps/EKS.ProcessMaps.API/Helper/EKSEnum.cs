namespace EKS.ProcessMaps.Helper.Enum
{
    /// <summary>
    /// AssetStatuses
    /// </summary>
    public enum AssetStatuses
    {
        Draft = 1,
        Published,
        SubmittedForApproval,
        ApprovedWaitingForJC
    }

    /// <summary>
    /// RelatedPeople
    /// </summary>
    public enum RelatedPeople
    {
        A = 1,
        O,
        C
    }

    /// <summary>
    /// AssetPartTypeCode
    /// </summary>
    public enum AssetPartTypeCode
    {
        PP = 1,
        LL,
        CC,
        DD,
        IBV,
        RR,
        TOC,
        CN,
        NOC,


        PH,  // Physics Or How It Works
        HY  // History



    }

    /// <summary>
    /// AssetType
    /// </summary>
    public enum AssetTypes
    {
        WI = 1,
        GB = 2,
        DS = 3,
        M = 4,
        AP = 6,
        CG = 10,
        RC = 12,
        TC = 11,
        KP = 9,
        SF = 13,
        SP = 14,
    }

    /// <summary>
    /// PublishedAssetStatuses
    /// </summary>
    public enum PublishedAssetStatus
    {
        Current = 1,
        Archived = 2,
        Obsolete = 3,
        Hidden = 4,
        WIP = 5,
        Legacy = 6
    }

    /// <summary>
    /// FileNames
    /// </summary>
    public enum FileNames
    {
        ExportWI,
        ExportCG

    }

    /// <summary>
    /// PWEmployments
    /// </summary>
    public enum PWEmployments
    {
        CONT,
        DISABLED,
        HRLE,
        HRNL,
        NHR
    }

    public enum ActivityBlockTypes
    {
        Activity = 1,
        Start = 2,
        End = 3,
        Decision = 4,
        Terminator = 5,
        Milestone = 6,
        EmptyBlock = 7,
        Step = 8
    }

    public enum PublishedAssetTypeCode
    {
        A = 1,
        C = 2,
        F = 3,
        G = 4,
        I = 5,
        K = 6,
        M = 7,
        P = 8,
        R = 9,
        S = 10,
        T = 11
    }
}