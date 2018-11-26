using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using xTab.Tools.Helpers;

namespace xTab.Tools.Extensions
{
    public static partial class ExtensionMethods
    {
        public static MvcHtmlString SimpleTextBox(
            this HtmlHelper helper,
            string name,
            object value,
            string title = null,
            object htmlAttributes = null,
            bool addClearButton = false,
            string clearButtonClass = "btn-default",
            string helpText = null,
            string helpTextPosition = "right",
            string addonText = null,
            string addonPosition = "right")
        {
            var controlHtml = helper.TextBox(name, value, htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup(title, controlHtml, addClearButton, clearButtonClass, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleTextBoxFor<TModel, TValue>(
            this HtmlHelper<TModel> helper,
            Expression<Func<TModel, TValue>> expression,
            String title = null,
            object htmlAttributes = null,
            bool addClearButton = false,
            string clearButtonClass = "btn-default",
            string helpText = null,
            string helpTextPosition = "right",
            string addonText = null,
            string addonPosition = "right")
        {
            var controlHtml = helper.TextBoxFor(expression, htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, addClearButton, clearButtonClass, helpText, helpTextPosition, addonText, addonPosition);
            
            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleTextAreaFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           String title = null,
           object htmlAttributes = null,
           string helpText = null,
           string helpTextPosition = "right")
        {
            var controlHtml = helper.TextAreaFor(expression, htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, false, null, helpText, helpTextPosition);
            
            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimplePasswordFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           String title = null,
           object htmlAttributes = null,
           bool addClearButton = false,
           string clearButtonClass = "btn-default",
           string helpText = null,
           string helpTextPosition = "right")
        {
            var controlHtml = helper.PasswordFor(expression, htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, addClearButton, clearButtonClass, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleDropDownListFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           List<SelectListItem> options,
           String title = null,
           object htmlAttributes = null,
            bool addClearButton = false,
            string clearButtonClass = "btn-default",
            string helpText = null,
            string helpTextPosition = "right")
        {
            var controlHtml = helper.DropDownListFor(expression, options ?? new List<SelectListItem>(), htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, addClearButton, clearButtonClass, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleDropDownList(
            this HtmlHelper helper,
            string name,            
            List<SelectListItem> options,
            string title = null,
            object htmlAttributes = null,
            bool addClearButton = false,
            string clearButtonClass = "btn-default",
            string helpText = null,
            string helpTextPosition = "right")
        {
            var controlHtml = helper.DropDownList(name, options ?? new List<SelectListItem>(), htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup(title, controlHtml, addClearButton, clearButtonClass, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleListBoxFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           List<SelectListItem> options,
           String title = null,
           object htmlAttributes = null,
            bool addClearButton = false,
            string clearButtonClass = "btn-default",
            string helpText = null,
            string helpTextPosition = "right")
        {
            var controlHtml = helper.ListBoxFor(expression, options, htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, addClearButton, clearButtonClass, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleCheckboxFor<TModel>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, bool>> expression,
           String title = null,
           String text = null,
           object htmlAttributes = null,
           string helpText = null,
           string helpTextPosition = "right")
        {
            var html = new StringBuilder();

            var checkBox = new TagBuilder("div");

            checkBox.AddCssClass("checkbox");

            checkBox.InnerHtml = html
                .Append("<label>")
                .Append(helper.CheckBoxFor(expression, htmlAttributes).ToHtmlString())
                .Append(" " + text)
                .Append("</label>")
                .ToString();

            var controlHtml = checkBox.ToString();
            var control = WrapInFormGroup<TModel, bool>(helper, expression, title, controlHtml, false, null, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleRadioFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           TValue value,
           String title = null,
           String text = null,
           object htmlAttributes = null,
           string helpText = null,
           string helpTextPosition = "right")
        {
            var html = new StringBuilder();

            var checkBox = new TagBuilder("div");
            var radioHtml = helper.RadioButtonFor(expression, value, htmlAttributes).ToHtmlString();
            checkBox.AddCssClass("radio");

            checkBox.InnerHtml = html
                .Append("<label>")
                .Append(radioHtml)
                .Append(" " + text)
                .Append("</label>")
                .ToString();

            var controlHtml = checkBox.ToString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, false, null, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleUploaderFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           String title = null,
           ImageUploaderOptions options = null,
           object htmlAttributes = null,
           string helpText = null,
           string helpTextPosition = "right")
        {
            var controlHtml = helper.ImageUploaderFor(expression, options, htmlAttributes).ToHtmlString();
            var control = WrapInFormGroup<TModel, TValue>(helper, expression, title, controlHtml, false, null, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }

        public static MvcHtmlString SimpleMapFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> latExpression,
           Expression<Func<TModel, TValue>> lngExpression,
           String title = null,
           string mapClass = "bs-map",
           bool showSearchBox = true,
           int mapHeight = 300,
           int zoom = 16,
           string formId = null,
           string helpText = null,
           string helpTextPosition = "right")
        {
            var html = new StringBuilder();  
            var mapTag = new TagBuilder("div");
         
            mapTag.AddCssClass(mapClass);
            mapTag.InnerHtml = html
                .Append(showSearchBox ? "<input class=\"form-control map-search\" type=\"text\" />" : null)
                .Append("<div class=\"map-container\" style=\"height:" + mapHeight + "px\" data-zoom=\"" + zoom + "\"></div>")
                .Append(String.IsNullOrEmpty(formId) ? helper.HiddenFor(latExpression, new { @class = "input-lat" }).ToHtmlString() : helper.HiddenFor(latExpression, new { @class = "input-lat", form = formId }).ToHtmlString())
                .Append(String.IsNullOrEmpty(formId) ? helper.HiddenFor(lngExpression, new { @class = "input-lng" }).ToHtmlString() : helper.HiddenFor(lngExpression, new { @class = "input-lng", form = formId }).ToHtmlString())
                .Append(helper.ValidationMessageFor(latExpression).ToHtmlString())
                .Append(helper.ValidationMessageFor(lngExpression).ToHtmlString())
                .ToString();

            var control = WrapInFormGroup(title, mapTag.ToString(), false, null, helpText, helpTextPosition);

            return MvcHtmlString.Create(control);
        }


        public static MvcHtmlString ImageUploaderFor<TModel, TValue>(
           this HtmlHelper<TModel> helper,
           Expression<Func<TModel, TValue>> expression,
           ImageUploaderOptions options = null,
           object htmlAttributes = null)
        {
            var html = new StringBuilder();
            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
            var panelTag = new TagBuilder("div");
            var panelBodyTag = new TagBuilder("div");
            var imgTag = new TagBuilder("img");
            var panelFooterTag = new TagBuilder("div");
            var panelFooterHtml = new StringBuilder();

            options = options ?? new ImageUploaderOptions();

            panelTag.AddCssClass("upload-panel " + options.ContainerCssClass);
            panelTag.Attributes.Add("id", options.ContainerId ?? RandomHelper.RandomString(10));
            panelTag.Attributes.Add("data-upload-link", options.UploadUrl);
            panelTag.Attributes.Add("data-min-width", options.MinWidth.ToString());
            panelTag.Attributes.Add("data-max-width", options.MaxWidth.ToString());
            panelTag.Attributes.Add("data-crop-width", options.CropWidth.ToString());
            panelTag.Attributes.Add("data-min-height", options.MinHeight.ToString());
            panelTag.Attributes.Add("data-max-height", options.MaxHeight.ToString());
            panelTag.Attributes.Add("data-crop-height", options.CropHeight.ToString());

            imgTag.AddCssClass("img-upload");
            imgTag.Attributes.Add("style", "max-height:" + options.ContainerHeight + "px");
            imgTag.Attributes.Add("src", metaData.Model != null ? (options.IsImageUploader ? metaData.Model.ToString() : options.UploadedFileImageSrc) : (options.IsImageUploader ? options.NoPicImageSrc : options.NoFileImageSrc));
            imgTag.Attributes.Add("data-uploading-img", options.UploadingImageSrc);
            imgTag.Attributes.Add("data-nopic-img", options.IsImageUploader ? options.NoPicImageSrc : options.NoFileImageSrc);
            imgTag.Attributes.Add("data-original", metaData.Model != null ? ((options.CropWidth > 0 && options.CropHeight > 0) ? IoHelper.GetSubName(metaData.Model.ToString(), "_original") : metaData.Model.ToString()) : String.Empty);

            panelBodyTag.AddCssClass("upload-area rel" + (metaData.Model != null ? " uploaded" : null));
            panelBodyTag.Attributes.Add("style", String.Format("height:{0}px;line-height:{0}px;", options.ContainerHeight));
            panelBodyTag.InnerHtml = imgTag.ToString() + html
                .Append("<div class=\"upload-area-btns\">")
                    .Append(options.AllowEdit ? ("<span style=\"display:" + (metaData.Model != null ? "inline-block" : "none") + "\" class=\"btn-edit\" title=\"Edit\"><span class=\"" + options.EditButtonCssClass + "\"></span>" + options.EditButtonText + "</span>") : null)
                    .Append("<a href=\"" + metaData.Model + "\" target=\"_blank\" style=\"display:" + (metaData.Model != null ? "inline-block" : "none") + "\" class=\"btn-view\" title=\"").Append("View").Append("\"><span class=\"").Append(options.ViewButtonCssClass).Append("\"></span>").Append(options.ViewButtonText).Append("</a>")
                    .Append("<span style=\"display:" + (metaData.Model != null ? "inline-block" : "none") + "\" class=\"btn-delete\" title=\"").Append("Delete").Append("\"><span class=\"").Append(options.DeleteButtonCssClass).Append("\"></span>").Append(options.DeleteButtonText).Append("</span>")
                .Append("</div>")
                .Append(options.AllowEdit ? "<div class='upload-remodal remodal-sm'></div>" : null).ToString();

            panelFooterTag.AddCssClass("upload-controls");
            panelFooterTag.InnerHtml = panelFooterHtml
                .Append("<div class=\"input-group\">")
                    .Append(helper.TextBoxFor(expression, htmlAttributes).ToHtmlString())
                    .Append("<div class=\"input-group-btn\">")
                        .Append("<span style=\"display:" + (metaData.Model != null ? "block" : "none") + "\" class=\"btn-delete\" title=\"").Append("Delete").Append("\"><span class=\"").Append(options.DeleteButtonCssClass).Append("\"></span>").Append(options.DeleteButtonText).Append("</span>")
                        .Append("<button type=\"button\" class=\"btn btn-" + options.UploadButtonBsColor + " btn-upload\" title=\"").Append("Upload").Append("\"><span class=\"").Append(options.UploadButtonCssClass).Append("\"></span>").Append(options.UploadButtonText).Append("</button>")
                    .Append("</div>")
                .Append("</div>").ToString();

            
            panelTag.InnerHtml += panelBodyTag.ToString();
            panelTag.InnerHtml += panelFooterTag.ToString();

            return MvcHtmlString.Create(panelTag.ToString());
        }


        private static String WrapInFormGroup<TModel, TValue>(HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, String title, String controlHtml, Boolean addClearButton = false, String clearButtonClass = "btn-default", String helpText = null, string helpTextPosition = "right", string addonText = null, string addonPosition = "right")
        {
            var resultControlHtml = controlHtml;
            var html = new StringBuilder();
            var formGroup = new TagBuilder("div");
            var inputHtml = new StringBuilder();
            var inputGroup = new TagBuilder("div");

            inputGroup.AddCssClass("input-group");

            if (!String.IsNullOrEmpty(addonText) || addClearButton)
            {
                if (!String.IsNullOrEmpty(addonText) && addonPosition == "left")
                {
                    inputHtml
                        .Append("<span class=\"input-group-addon\">")
                        .Append(addonText)
                        .Append("</span>");
                }

                inputHtml.Append(controlHtml);

                if (!String.IsNullOrEmpty(addonText) && addonPosition == "right")
                {
                    inputHtml
                        .Append("<span class=\"input-group-addon\">")
                        .Append(addonText)
                        .Append("</span>");
                }

                if (addClearButton)
                {
                    inputHtml                        
                        .Append("<span class=\"input-group-btn\">")
                            .Append("<button class=\"input-clear btn " + clearButtonClass + "\" type=\"button\">")
                                .Append("<i class=\"fa fa-fw fa-times\"></i>")
                            .Append("</button>")
                        .Append("</span>");                   
                }


                inputGroup.InnerHtml = inputHtml.ToString(); 
                resultControlHtml = inputGroup.ToString();
            }
           
            formGroup.AddCssClass("form-group");

            if (!String.IsNullOrEmpty(title))
                html = html.Append(helper.LabelFor(expression, title, new { @class = "control-label" }).ToHtmlString());
            else if (title == String.Empty)
                html = html.Append("<label class='control-label'>&nbsp;</label>");
         
            formGroup.InnerHtml = html
                .Append(String.IsNullOrEmpty(helpText) ? null : "<a role='button' tabindex='0' class='help-tooltip' data-toggle='popover' data-placement='" + helpTextPosition + "' data-trigger='focus' data-content='" + helpText + "'><i class='fa fa-fw fa-question-circle'></i></a>")
                .Append(resultControlHtml)
                .Append(helper.ValidationMessageFor(expression).ToHtmlString())
                .ToString();

            return formGroup.ToString();
        }

        private static String WrapInFormGroup(String title, String controlHtml, Boolean addClearButton = false, String clearButtonClass = "btn-default", String helpText = null, string helpTextPosition = "right")
        {
            var resultControlHtml = controlHtml;
            var html = new StringBuilder();
            var formGroup = new TagBuilder("div");

            if (addClearButton)
            {
                var inputHtml = new StringBuilder();
                var inputGroup = new TagBuilder("div");

                inputGroup.AddCssClass("input-group");
                inputGroup.InnerHtml = inputHtml
                    .Append(controlHtml)
                    .Append("<span class=\"input-group-btn\">")
                        .Append("<button class=\"input-clear btn " + clearButtonClass + "\" type=\"button\">")
                            .Append("<i class=\"fa fa-fw fa-times\"></i>")
                        .Append("</button>")
                    .Append("</span>").ToString();

                resultControlHtml = inputGroup.ToString();
            }

            formGroup.AddCssClass("form-group");

            if (!String.IsNullOrEmpty(title))
                html = html.Append("<label class='control-label'>" + title + "</label>");
            else if (title == String.Empty)
                html = html.Append("<label class='control-label'>&nbsp;</label>");

            formGroup.InnerHtml = html
                .Append(String.IsNullOrEmpty(helpText) ? null : "<a role='button' tabindex='0' class='help-tooltip' data-toggle='popover' data-placement='" + helpTextPosition + "' data-trigger='focus' data-content='" + helpText + "'><i class='fa fa-fw fa-question-circle'></i></a>")
                .Append(resultControlHtml)
                .ToString();

            return formGroup.ToString();
        }
    }



    public class ImageUploaderOptions
    {
        public ImageUploaderOptions()
        {
            UploadingImageSrc = "http://cdn.bissoft.org/img/parts/line-loading.gif";
            NoPicImageSrc = "http://cdn.bissoft.org/img/parts/no-pic.png";
            NoFileImageSrc = "http://cdn.bissoft.org/img/parts/no-file.png";
            UploadedFileImageSrc = "http://cdn.bissoft.org/img/parts/file-icon.png";
        }

        public String UploadUrl { get; set; } = "/api/upload/uploadimage";
        public String BrowseUrl { get; set; } = "/upload/";
        public String EditUrl { get; set; } = "/api/upload/editimage";
        public String ContainerId { get; set; } = RandomHelper.RandomString(10);
        public String ContainerCssClass { get; set; } = null;
        public Int32 ContainerHeight { get; set; } = 141;
        public String UploadButtonBsColor { get; set; } = "primary";
        public String UploadButtonCssClass { get; set; } = "fa fa-fw fa-upload";
        public String UploadButtonText { get; set; } = null;
        public String DeleteButtonCssClass { get; set; } = "fa fa-fw fa-trash-o";
        public String DeleteButtonText { get; set; } = null;
        public String ViewButtonCssClass { get; set; } = "fa fa-fw fa-eye";
        public String ViewButtonText { get; set; } = null;
        public String BrowseButtonCssClass { get; set; } = "fa fa-fw fa-folder-open-o";
        public String BrowseButtonText { get; set; } = null;
        public String EditButtonCssClass { get; set; } = "fa fa-fw fa-pencil";
        public String EditButtonText { get; set; } = null;
        public String UploadingImageSrc { get; set; }
        public String NoPicImageSrc { get; set; }
        public String NoFileImageSrc { get; set; }
        public String UploadedFileImageSrc { get; set; }
        public Boolean IsImageUploader { get; set; } = true;
        public Boolean AllowBrowse { get; set; } = false;
        public Boolean AllowEdit { get; set; } = false;

        public Boolean SquareThumbs { get; set; } = false;
        public Boolean LandscapeThumbs { get; set; } = false;
        public Boolean PortraitThumbs { get; set; } = false;

        public String MaxFileSize { get; set; } = "50";
        public Int32 MaxWidth { get; set; } 
        public Int32 MaxHeight { get; set; }
        public Int32 MinWidth { get; set; }
        public Int32 MinHeight { get; set; }
        public Int32 CropWidth { get; set; }
        public Int32 CropHeight { get; set; }
    }
}