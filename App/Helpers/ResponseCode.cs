using System;

namespace Project.App.Helpers
{
    public static partial class ResponseCode
    {
        #region LicenseCheckMiddleware
        public const string LicenseInValid = "Server.License.InValid";
        public const string LicenseDataExpire = "Server.LicenseData.Expire";
        #endregion
        #region Account
        public const string AccountTwoFactorAuthInValid = "Server.AccountTwoFactorAuth.InValid"; 
        public const string AccountCareerInValid = "Server.AccountCareer.InValid"; 
        public const string AccountIdsIsRequired = "Server.AccountIds.IsRequired";



        public const string AccountIdsInValid = "Server.AccountIds.InValid";
        public const string AccountUserNameIsRequired = "Server.AccountUserName.IsRequired";
        public const string AccountUserNameAlreadyExists = "Server.AccountUserName.AlreadyExists";
        public const string AccountPhoneIsRequired = "Server.AccountPhone.IsRequired";
        public const string AccountPhoneAlreadyExists = "Server.AccountPhone.AlreadyExists";
        public const string AccountEmailIsRequired = "Server.AccountEmail.IsRequired";
        public const string AccountEmailAlreadyExists = "Server.AccountEmail.AlreadyExists";
        public const string AccountFullNameIsRequired = "Server.AccountFullName.IsRequired";
        public const string AccountPasswordIsRequired = "Server.AccountPassword.IsRequired";
        public const string AccountIdDoesNotExists = "Server.AccountId.DoesNotExists";
        public const string AccountEmailMustBeEmail = "Server.AccountEmail.MustBeEmail";
        public const string AccountIdInActive = "Server.AccountId.InActive";
        public const string AccountIdNotFound = "Server.AccountId.NotFound";
        public const string StoreAccountSuccessfully = "Server.StoreAccount.Successfully";
        public const string UpdateAccountSuccessfully = "Server.UpdateAccount.Successfully";
        public const string GetProfileAccountSuccessfully = "Server.GetProfileAccount.Successfully";
        public const string DeleteAccountsSuccessfully = "Server.DeleteAccounts.Successfully";
        public const string GetAccountsSuccessfully = "Server.GetAccounts.Successfully";
        public const string AccountUserNameMussLessThan256Character = "Server.AccountUserName.MussLessThan256Character";
        public const string AccountFullNameMussLessThan256Character = "Server.AccountFullName.MussLessThan256Character";
        public const string AccountPhoneMustHaveNummericCharacterOrPlusCharacter = "Server.AccountPhone.MustHaveNummericCharacterOrPlusCharacter";
        public const string AccountIdentityCardMustNumericCharacter = "Server.AccountIdentityCard.MustNumericCharacter";
        public const string AccountIdentityCardMustLessThan13Character = "Server.AccountIdentityCard.MustLessThan13Character";
        public const string AccountPhoneMaxLength = "Server.AccountPhone.MaxLength";

        #endregion

        #region Role
        public const string RoleIdIsRequired = "Server.RoleId.IsRequired";
        public const string RoleIdDoesNotExists = "Server.RoleId.DoesNotExists";
        public const string RoleIdsInvalid = "Server.RoleIds.Invalid";
        public const string RoleIdsAlreadyUsed = "Server.RoleIds.AlreadyUsed";
        public const string RoleIdsIsRequired = "Server.RoleIds.IsRequired";
        public const string RoleNameIsRequired = "Server.RoleName.IsRequired";
        public const string RoleCodeIsRequired = "Server.RoleCode.IsRequired";
        public const string RoleCodeAlreadyExists = "Server.RoleCode.AlreadyExists";
        public const string GetRolesSuccessfully = "Server.GetRoles.Successfully";
        public const string GetRoleSuccessfully = "Server.GetRole.Successfully";
        public const string StoreRoleSuccessfully = "Server.StoreRole.Successfully";
        public const string UpdateRoleSuccessfully = "Server.UpdateRole.Successfully";
        public const string DeleteRolesSuccessfully = "Server.DeleteRoles.Successfully";
        public const string RoleNameMustLessThan256Character = "Server.RoleName.MustLessThan256Character";
        public const string RoleCodeMustLessThan256Character = "Server.RoleCode.MustLessThan256Character";
        #endregion

        #region Permission
        public const string PermissionCodesInvalid = "Server.PermissionCodes.Invalid";
        public const string PemissionCodesIsRequired = "Server.PemissionCodes.IsRequired";
        public const string GetPermissionsSuccessfully = "Server.GetPermissions.Successfully";
        #endregion

        #region Area
        public const string AreaIdIsRequired = "Server.AreaId.IsRequired";
        public const string AreaIdDoesNotExists = "Server.AreaId.DoesNotExists";
        public const string AreaDeactive = "Server.Area.Deactive";
        public const string ParentAreaDeactive = "Server.ParentArea.Deactive";
        public const string LicenseExpirationAtHasExpired = "Server.LicenseExpirationAt.HasExpired";
        #endregion

        #region Media
        public const string MediaInfoIsRequired = "Server.MediaInfo.IsRequired";
        public const string MediaAlreadyApproved = "Server.Media.AlreadyApproved";
        public const string MediaIdsIsRequired = "Server.MediaIds.IsRequired";
        public const string MediaIdsInvalid = "Server.MediaIds.Invalid";
        public const string MediaIdInUse = "Server.MediaIds.InUse";
        public const string AreaIdsMustChildCurrentArea = "Server.AreaIds.MustChildCurrentArea";
        public const string AreaIdsDoesNotExists = "Server.AreaIds.DoesNotExists";
        public const string AreaIdsInvalid = "Server.AreaIds.Invalid";
        public const string MediaStatusIsRequired = "Server.MediaStatus.IsRequired";
        public const string MediaStatusInValid = "Server.MediaStatus.InValid";
        public const string MediaIdDoesNotExists = "Server.MediaId.DoesNotExists";
        public const string FieldIdDoesNotExists = "Server.FieldId.DoesNotExists";
        public const string MediaContentHasSizeExceedTheAllowedLimit = "Server.MediaContent.HasSizeExceedTheAllowedLimit";
        public const string MediaContentTypeDoesNotSupportToUpload = "Server.MediaContent.TypeDoesNotSupportToUpload";
        public const string MediaResourceUrlMustBeValidUrl = "Server.MediaResourceUrl.MustBeValidUrl";
        public const string IsDownloadIsRequired = "Server.IsDownload.IsRequired";
        public const string FieldIdIsRequired = "Server.FieldId.IsRequired";
        public const string MediaNameIsRequired = "Server.MediaName.IsRequired";
        public const string MediaContentIsRequired = "Server.MediaContent.IsRequired";
        public const string MediaResourceUrlIsRequired = "Server.MediaResourceUrl.IsRequired";
        public const string MediaContentFileDoesNotSupportToUpload = "Server.MediaContent.FileDoesNotSupportToUpload";
        public const string GetMediaFormatsSuccessfully = "Server.GetMediaFormats.Successfully";
        public const string DeleteMediasSuccessfully = "Server.DeleteMedias.Successfully";
        public const string UpdateMediaSuccessfully = "Server.UpdateMedia.Successfully";
        public const string UploadMediaSuccessfully = "Server.UploadMedias.Successfully";
        public const string GetMediasSuccessfully = "Server.GetMedias.Successfully";
        public const string GetMediaSuccessfully = "Server.GetMedia.Successfully";
        public const string GetMediaSharesSuccessfully = "Server.GetMediaShares.Successfully";
        public const string ShareMediaSuccessfully = "Server.ShareMedia.Successfully";
        public const string DoesNotPermissionWithMediaIds = "Server.DoesNotPermissionWith.MediaIds";
        public const string VoiceIsRequired = "Server.Voice.IsRequired";
        public const string VoiceInValid = "Server.Voice.InValid";
        public const string SpeedInValid = "Server.Speed.InValid";
        public const string MediaInfoInValid = "Server.MediaInfo.InValid";
        public const string MediaNameMustLessThan256Character = "Server.MediaName.MustLessThan256Character";
        public const string MediaDescriptionMustLessThan256Character = "Server.MediaDescription.MustLessThan256Character";


        #endregion

        #region Playlist
        public const string PlaylistNameMustLessThan256Character = "Server.PlaylistName.MustLessThan256Character";
        public const string PlaylistDescriptionMustLessThan256Character = "Server.PlaylistDescription.MustLessThan256Character";
        public const string PlaylistIdsIsRequired = "Server.PlaylistIds.IsRequired";
        public const string PlaylistIdsIsAlreadyInSchedule = "Server.PlaylistIds.AlreadyInSchedule";
        public const string PlaylistIdsInValid = "Server.PlaylistIds.InValid";
        public const string PlaylistDetailOrderIsRequired = "Server.PlaylistDetailOrder.IsRequired";
        public const string IdMustBeAudio = "Server.Id.MustBeAudio";
        public const string IdMustBeText = "Server.Id.MustBeText";
        public const string PlaylistNameIsRequired = "Server.PlaylistName.IsRequired";
        public const string PlaylistTypeIsRequired = "Server.PlaylistType.IsRequired";
        public const string PlaylistSourceIsRequired = "Server.PlaylistSource.IsRequired";
        public const string PlaylistTypeInValid = "Server.PlaylistType.InValid";
        public const string PlaylistSourceInValid = "Server.PlaylistSource.InValid";
        public const string PlaylistDetailsIsRequired = "Server.PlaylistDetails.IsRequired";
        public const string PlaylistIdIsRequired = "Server.PlaylistId.IsRequired";
        public const string PlaylistIdDoesnotExists = "Server.PlaylistId.DoesnotExists";
        public const string PlaylistAlreadyUsed = "Server.Playlist.AlreadyUsed";
        public const string PlaylistsAlreadyUsed = "Server.Playlists.AlreadyUsed";
        public const string DeletePlaylistSuccessfully = "Server.DeletePlaylist.Successfully.";
        public const string UpdatePlaylistSuccessfully = "Server.UpdatePlaylist.Successfully";
        public const string StorePlaylistSuccessfully = "Server.StorePlaylist.Successfully";
        public const string GetPlaylistSuccessfully = "Server.GetPlaylist.Successfully";
        public const string GetPlaylistsSuccessfully = "Server.GetPlaylists.Successfully";
        public const string DeletePlaylistsSuccessfully = "Server.DeletePlaylists.Successfully";
        public const string GetTemplatesSuccessfully = "Server.GetTemplates.Successfully";
        public const string DoesNotPermissionWithPlaylistIds = "Server.DoesNotPermissionWith.PlaylistIds";

        #endregion

        #region Share

        public const string IdIsRequired = "Server.Id.Required";
        public const string IdDoesNotExists = "Server.Id.DoesNotExists";
        public const string IdDoesNotApprove = "Server.Id.DoesNotApprove";

        #endregion

        #region Template
        public const string TemplateNameMustLessThan256Character = "Server.TemplateName.MustLessThan256Character";
        public const string DoesNotPermissionWithTemplateIds = "Server.DoesNotPermissionWith.TemplateIds";
        public const string GetSharesSuccessfully = "Server.GetShares.Successfully";
        public const string TeamplateNameIsRquired = "Server.TemplateName.IsRquired";
        public const string TemplateResolutionWidthIsRequired = "Server.TemplateResolutionWidth.IsRequired";
        public const string TemplateResolutionWidthMustGreaterThan0 = "Server.TemplateResolutionWidth.MustGreaterThan0";
        public const string TemplateResolutionHeightIsRequired = "Server.TemplateResolutionHeight.IsRequired";
        public const string TemplateResolutionHeightMustGreaterThan0 = "Server.TemplateResolutionHeight.MustGreaterThan0";
        public const string TemplateMediasIsRequired = "Server.TemplateMedias.IsRequired";
        public const string TemplateIdsIsRequired = "Server.TemplateIds.IsRequired";
        public const string MediaIdIsRequired = "Server.MediaId.IsRequired";
        public const string MediaIdDoesNotApprove = "Server.MediaId.DoesNotApprove";
        public const string TemplateMediaSizeWidthIsRequired = "Server.TemplateMediaSizeWidth.IsRequired";
        public const string TemplateMediaSizeWidthMustGreatherThan0 = "Server.TemplateMediaSizeWidth.MustGreatherThan0";
        public const string TemplateMediaSizeWidthMustLessThanOrEqualTo1 = "Server.TemplateMediaSizeWidth.MustLessThanOrEqualTo1";
        public const string TemplateMediaSizeHeightIsRequired = "Server.TemplateMediaSizeHeight.IsRequired";
        public const string TemplateMediaSizeHeightMustGreaterThan0 = "Server.TemplateMediaSizeHeight.MustGreaterThan0";
        public const string TemplateMediaSizeHeightLessThanOrEqualTo1 = "Server.TemplateMediaSizeHeight.LessThanOrEqualTo1";
        public const string TemplateMediaZIndexIsRequired = "Server.TemplateMediaZIndex.IsRequired";
        public const string TemplateMediaPositionXIsRequired = "Server.TemplateMediaPositionX.IsRequired";
        public const string TemplateMediaPositionXMustGreatherThanOrEqualTo0 = "Server.TemplateMediaPositionX.MustGreatherThanOrEqualTo0";
        public const string TemplateMediaPositionXMustLessThanOrEqualTo1 = "Server.TemplateMediaPositionX.MustLessThanOrEqualTo1";
        public const string TemplateMediaPositionYIsRequired = "Server.TemplateMediaPositionY.IsRequired";
        public const string TemplateMediaPositionYMustGreatherThanOrEqualTo0 = "Server.TemplateMediaPositionY.MustGreatherThanOrEqualTo0";
        public const string TemplateMediaPositionYMustLessThanOrEqualTo1 = "Server.TemplateMediaPositionY.MustLessThanOrEqualTo1";
        public const string TemplateAlreadyUsed = "Server.Template.AlreadyUsed";
        public const string TemplateIdIsRequired = "Server.TemplateId.IsRequired";
        public const string TemplateIdDoesNotExists = "Server.TemplateId.DoesNotExists";
        public const string StoreTemplateSuccessfully = "Server.StoreTemplate.Successfully";
        public const string GetTemplateSuccessfully = "Server.GetTemplate.Successfully";
        public const string DeleteTemplatesSuccessfully = "Server.DeleteTemplates.Successfully";
        public const string UpdateTemplateSuccessfully = "Server.UpdateTemplate.Successfully";

        #endregion

        #region Post
        public const string SyncPostStatusDoesNotHaveValue = "Server.SyncPostStatus.DoesNotHaveValue"; 
        public const string PostFilesInValid = "Server.PostFiles.InValid"; 
        public const string PostDescriptionIsRequired = "Server.PostDescription.IsRequired";
        public const string PostDescriptionMustLessThan256Character = "Server.PostDescription.MustLessThan256Character";
        public const string PostTitleIsRequired = "Server.PostTitle.IsRequired";
        public const string PostTitleMustLessThan256Character = "Server.PostTitle.MustLessThan256Character";
        public const string PostCodeIsRequired = "Server.PostCode.IsRequired";
        public const string PostCodeAlreadyExist = "Server.PostCode.AlreadyExist";
        public const string PostCodeMustLessThan256Character = "Server.PostCode.MustLessThan256Character";
        public const string PostSourceIsRequired = "Server.PostSource.IsRequired";
        public const string PostSourceMustLessThan256Character = "Server.PostSource.MustLessThan256Character";
        public const string PostCoverInValidContentType = "Server.PostCover.InValidContentType";
        public const string AddPostSuccessfully = "Server.AddPost.Successfully";
        public const string PostIdNotFound = "Server.PostId.NotFound";
        public const string GetPostSuccessfully = "Server.GetPost.Successfully";
        public const string UpdatePostSuccessfully = "Server.UpdatePost.Successfully";
        public const string PostContentIsRequired = "Server.PostContent.IsRequired";
        public const string PostIdAlreadyPublish = "Server.PostId.AlreadyPublish";
        public const string PostIdAlreadyApprove = "Server.PostId.AlreadyApprove";
        public const string PostIdsInValid = "Server.PostIds.InValid";
        public const string PostIdNotApprove = "Server.PostId.NotApprove";
        public const string PostIdsIsRequired = "Server.PostIds.IsRequired";
        public const string DeletePostsSuccessfully = "Server.DeletePosts.Successfully";
        public const string GetPostsSuccessfully = "Server.GetPosts.Successfully";
        public const string SharePostSuccessfully = "Server.SharePost.Successfully";
        public const string PostIdsAlreadyApprove = "Server.PostIds.AlreadyApprove";
        public const string PostStatusInValid = "Server.Post.Status.InValid";
        public const string PostIdsNotApprove = "Server.PostIds.NotApprove";
        public const string ApprovePostsSuccessfully = "Server.ApprovePosts.Successfully";
        public const string PublishPostsSuccessfully = "Server.PublishPosts.Successfully";
        public const string DoesNotPemissionWithPostId = "Server.DoesNotPemissionWith.PostId";
        public const string DoesNotPemissionWithPostIds = "Server.DoesNotPemissionWith.PostIds";
        #endregion

        #region Feedback
        public const string FeedbackEmailIsRequired = "Server.FeedbackEmail.IsRequired";
        public const string FeedbackEmailMustBeEmailAddress = "Server.FeedbackEmail.MustBeEmailAddress";
        public const string FeedbackContentIsRequired = "Server.FeedbackContent.IsRequired";
        public const string FeedbackPhoneMustHaveNummericCharacterOrPlusCharacter = "Server.FeedbackPhone.MustHaveNummericCharacterOrPlusCharacter";
        public const string FeedbackPhoneIsRequired = "Server.FeedbackPhone.IsRequired";
        public const string FeedbackTitleIsRequired = "Server.FeedbackTitle.IsRequired";
        public const string FeedbackIdNotFound = "Server.FeedbackId.NotFound";
        public const string FeedbackAlreadyReplied = "Server.Feedback.AlreadyReplied";
        public const string MediaContentsIsRequired = "Server.MediaContents.IsRequired";
        public const string StoreFeedbackSuccess = "Server.StoreFeedback.Success";
        public const string FeedbackEmailMaxLength = "Server.FeedbackEmail.MaxLength";
        public const string FeedbackTitleMaxLength = "Server.FeedbackTitle.MaxLength";
        public const string GetFeedbackSuccess = "Server.GetFeedback.Success";
        public const string ReplyContentIsRequired = "Server.ReplyContent.IsRequired";
        public const string ReplyFeedbackSuccess = "Server.ReplyFeedback.Success";
        public const string GetFeedbacksSuccess = "Server.GetFeedbacks.Success";
        public const string FeedbackIdsIsRequired = "Server.FeedbackIds.IsRequired";
        public const string DeleteFeedbacksSuccess = "Server.DeleteFeedbacks.Success";
        public const string FeedbackIdsInvalid = "Server.FeedbackIds.Invalid";
        #endregion

        #region Survey
        public const string SurveyNameIsRequired = "Server.SurveyName.IsRequired";
        public const string AnswerContentIsRequired = "Server.AnswerContent.IsRequired";
        public const string QuestionTypeIsRequired = "Server.QuestionType.IsRequired";
        public const string AnswerContentsIsRequired = "Server.AnswerContents.IsRequired";
        public const string AnswerOrderIsRequired = "Server.AnswerOrder.IsRequired";
        public const string StoreSurveySuccess = "Server.StoreSurvey.Success";
        public const string SurveyStartIsRequired = "Server.SurveyStart.IsRequired";
        public const string SurveyEndIsRequired = "Server.SurveyEnd.IsRequired";
        public const string SurveyEndMustGreatherThanOrEqualToSurveyStart = "Server.SurveyEnd.MustGreatherThanSurveyStart";
        public const string QuestionTypeInValid = "Server.QuestionType.InValid";
        public const string SurveyIdsIsRequired = "Server.SurveyIds.IsRequired";
        public const string DeleteSurveysSuccess = "Server.DeleteSurveys.Success";
        public const string SurveyIdsInvalid = "Server.SurveyIds.Invalid";
        public const string GetSurveysSuccess = "Server.GetSurveys.Success";
        public const string SurveyIdIsRequired = "Server.SurveyId.IsRequired";
        public const string SurveyIdIsNotFound = "Server.SurveyIds.NotFound";
        public const string SurveyIsStarting = "Server.Survey.IsStarting";
        public const string SurveyIsEnd = "Server.Survey.IsEnd";
        public const string UpdateSurveySuccess = "Server.UpdateSurvey.Success";
        public const string QuestionIdNotFound = "Server.QuestionId.NotFound";
        public const string QuestionNotTypeOpen = "Server.Question.NotTypeOpen";
        public const string SurveyNotStarting = "Server.Survey.NotStarting";
        public const string QuestionsNotBelongToSurvey = "Server.Questions.NotBelongToSurvey";
        public const string QuestionsIsRequired = "Server.Questions.IsRequired";
        public const string QuestionNotBelongToSurvey = "Server.Question.NotBelongToSurvey";
        public const string AnswerNotBelongToQuestion = "Server.Answer.NotBelongToQuestion";
        public const string SurveySuccess = "Server.Survey.Success";
        public const string GetQuestionOpenSuccess = "Server.GetQuestionOpen.Success";
        public const string MustReplyAllQuestionIsRequired = "ResponseCode.QuestionIds.MustReplyAllQuestionIsRequired";
        #endregion

        public const string IdNotFound = "Server.IdNotFound";
        public const string ScheduleAlreadyPlaying = "Server.Schedule.AlreadyPlaying";
        public const string DeviceIPInvalid = "Server.DeviceIP.Invalid";
        public const string IsRequiredInvalid = "Server.IsRequired.Invalid";
        public const string SurveyEndMustGreaterThanOrEqualToNow = "Server.SurveyEnd.MustGreaterThanOrEqualToNow";
        public const string SurveyNameMaxLength = "Server.SurveyName.MaxLength";
        public const string QuestionContentMaxLength = "Server.QuestionContent.MaxLength";
        public const string AnswerContentMaxLength = "Server.AnswerContent.MaxLength";
        public const string TotalAnswerInvalid = "Server.TotalAnswer.Invalid";
        public const string AreaNotBelongToFeedback = "Server.Area.NotBelongToFeedback";
        public const string AreaNotAssignToReply = "Server.Area.NotAssignToReply";
        public const string AssignFeedbackSuccess = "Server.AssignFeedback.Success";
        public const string PostNotInThisArea = "Server.Post.NotInThisArea";
        public const string ContentConvertInValid = "Server.ContentConvert.InValid";
        public const string AreaHTTTCodeInValid = "Server.AreaHTTTCode.InValid";
        public const string AreaHTTTCodeNotFound = "Server.AreaHTTTCode.NotFound";
        public const string SourceIdInValid = "Server.SourceId.InValid";
        public const string SourceIdIsRequired = "Server.SourceId.IsRequired";
        public const string SourceIdWrongContentType = "Server.SourceId.WrongContentType";
        public const string ScheduleMustOnlySource = "Server.Schedule.MustOnlySource";
        public const string DurationIsRequired = "Server.Duration.IsRequired";
        public const string NotifyIdsIsRequired = "Server.NotifyIds.IsRequired";
        public const string NotifyIdsInValid = "Server.NotifyIds.InValid";
        public const string MonthFromIsRequired = "Server.MonthFrom.IsRequired";
        public const string MonthToIsRequired = "Server.MonthTo.IsRequired";
        public const string AvatarDoesNotSupportToUpload = "Server.Avatar.DoesNotSupportToUpload";
        public const string PostDocumentInValidContentType = "Server.PostDocument.InValidContentType";

        #region Document
        public const string GetDocumentSuccessfully = "Server.Document.GetSuccess";
        public const string DocumentNotFound = "Server.Document.NotFound";
        public const string DoesNotPermissionWithDocumentIds = "Server.DoesNotPermissionWith.DocumentIds";
        public const string DocumentInValid = "Server.Document.InValid";
        public const string DocumentNotAssigned = "Server.Document.NotAssigned";
        public const string DocumentNameIsRequired = "Server.DocumentName.IsRequired";
        public const string DocumentPinStatusInValid = "Server.DocumentPinStatus.InValid";
        public const string DocumentIdIsRequired = "Server.DocumentId.IsRequired";
        public const string DocumentAlrealyAssign = "Server.Document.AlrealyAssign";
        public const string DocumentContentIsRequired = "Server.Document.ContentIsRequired";
        public const string DocumentFileDoesNotFromSource = "Server.Document.FileDoesNotFromSource";
        public const string DocumentFileInValid = "Server.Document.FileInValid";
        public const string DocumentNameMaxLength = "Server.Document.NameMaxLength";
        public const string DateIssueIsRequired = "Server.DateIssue.IsRequired";
        public const string DocumentFromIsRequired = "Server.DocumentFrom.IsRequired";
        public const string DocumentFromGreatherThanNow = "Server.DocumentFrom.GreatherThanNow";
        public const string DocumentToGreaterThanFrom = "Server.DocumentTo.GreaterThanFrom";
        public const string DocumentToIsRequired = "Server.DocumentTo.IsRequired";
        public const string DocumentDateIssueIsRequired = "Server.DocumentDateIssue.IsRequired";
        public const string DocumentToMustGreaterThanDateIssue = "Server.DocumentTo.MustGreaterThanDateIssue";
        public const string DocumentPropertyDoesNotUpdatePermission = "Server.DocumentProperty.DoesNotUpdatePermission";
        public const string DocumentAbstractMaxLength = "Server.DocumentAbstract.MaxLength";
        public const string DocumentAbstractIsRequired = "Server.DocumentAbstract.IsRequired";
        public const string DocumentIsUsed = "Server.Document.IsUsed";
        #endregion
        #region BroadcastProgram
        public const string SyncBroadcastProgramStatusDoesNotHaveValue = "Server.SyncBroadcastProgramStatus.DoesNotHaveValue";
        public const string BroadcastProgramWasSentOrFinished = "Server.BroadcastProgram.WasSentOrFinished";
        public const string BroadcastProgramFinished = "Server.BroadcastProgram.Finished";
        public const string BroadcastProgramIdRequired = "Server.BroadcastProgramId.IsRequired";
        public const string BroadcastProgramIdNotFound = "Server.BroadcastProgramId.NotFound";
        public const string BroadcastProgramContentIsRequired = "Server.BroadcastProgramContent.IsRequired";
        public const string BroadcastProgramFilesIsRequired = "Server.BroadcastProgramFiles.IsRequired";
        public const string BroadcastProgramFilesInValid = "Server.BroadcastProgramFiles.InValid";
        public const string BroadcastProgramCodeAlreadyExist = "Server.BroadcastProgramCode.AlreadyExist";
        public const string BroadcastProgramCodeIsRequired = "Server.BroadcastProgramCode.IsRequired";
        public const string BroadcastProgramSourceIsRequired = "Server.BroadcastProgramSource.IsRequired";
        public const string BroadcastProgramTitleIsRequired = "Server.BroadcastProgramTitle.IsRequired";
        public const string BroadcastProgramTitleMaxLength = "Server.BroadcastProgramTitle.MaxLength";
        public const string BroadcastProgramStatusInValid = "Server.BroadcastProgramStatus.InValid"; 
        public const string BroadcastProgramStatusRequired = "Server.BroadcastProgramStatus.Required"; 
        public const string DoesNotPermissionWithBroadcastProgramIds = "Server.DoesNotPermissionWith.BroadcastProgramIds";
        public const string BroadcastProgramStatusApproved = "Server.BroadcastProgramStatus.Approved"; 
        public const string BroadcastProgramStatusRejectedOrApproved = "Server.BroadcastProgramStatus.RejectedOrApproved"; 
        public const string BroadcastProgramStatusNotApproved = "Server.BroadcastProgramStatus.NotApproved";
        public const string BroadcastProgramApprovedIdShouldBeEmpty = "Server.BroadcastProgramApprovedId.ShouldBeEmpty";
        public const string BroadcastProgramApprovedPointIsRequired = "Server.BroadcastProgramApprovedPoint.IsRequired";
        public const string BroadcastProgramApprovedPointMustFrom0To10 = "Server.BroadcastProgramApprovedPoint.MustFrom0To10";
        public const string BroadcastProgramRejectReasonIsRequired = "Server.BroadcastProgramRejectReason.IsRequired";
        public const string ApproveBroadcastProgramsSuccessfully = "Server.ApproveBroadcastPrograms.Successfully";
        public const string ApproveBroadcastProgramsFailed = "Server.ApproveBroadcastPrograms.Failed";
        public const string BroadcastProgramCodeMaxLength = "Server.BroadcastProgramCode.MaxLength";
        public const string BroadcastProgramSourceMaxLength = "Server.BroadcastProgramSource.MaxLength";
        public const string BroadcastProgramAuthorMaxLength = "Server.BroadcastProgramAuthor.MaxLength";
        public const string BroadcastProgramPosterMaxLength = "Server.BroadcastProgramPoster.MaxLength";
        #endregion
    }
}
