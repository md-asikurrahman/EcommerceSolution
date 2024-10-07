using System.Text;

namespace ECommerceSolution.Service.Extensions
{
    /// <summary>
	/// Summary description for TemplateHelper.
	/// </summary>
	public class TemplateHelper
    {
        //------------------------------ Public methods ----------------------
        /// <summary>
        /// Returns the section of strTemplate that begins with strBeginToken and ends with strEndToken
        /// </summary>
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionGet method in ITS.Common.TemplateHelper
        /// Note: the new API only supports the <!--[section]-->, <!--[/section]--> convention, 
        /// so templates using any other section style will need to be converted to use 
        /// the new convention before calling this API.
        /// </remarks>        
        public static string GetSection(string strTemplate, string strBeginToken, string strEndToken)
        {
            int intStart;
            int intEnd;
            string strSection;
            intStart = strTemplate.IndexOf(strBeginToken);
            if (intStart < 0)
            {
                strSection = "";
            }
            else
            {
                intEnd = strTemplate.IndexOf(strEndToken, intStart) + strEndToken.Length;
                strSection = strTemplate.Substring(intStart, intEnd - intStart);
            }
            return strSection;
        }
        /// <summary>
        /// Returns the section of strTemplate that begins with strBeginToken and ends with strEndToken
        /// </summary>
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionGet method in ITS.Common.TemplateHelper
        /// Note: the new API only supports the <!--[section]-->, <!--[/section]--> convention, 
        /// so templates using any other section style will need to be converted to use 
        /// the new convention before calling this API.
        /// </remarks>        
        public static StringBuilder GetSection(StringBuilder strbTemplate, string strBeginToken, string strEndToken)
        {
            int intStart;
            int intEnd;
            StringBuilder strbSection = new StringBuilder("");
            intStart = strbTemplate.ToString().IndexOf(strBeginToken);
            if (intStart >= 0)
            {
                intEnd = strbTemplate.ToString().IndexOf(strEndToken, intStart) + strEndToken.Length;
                strbSection.Append(strbTemplate.ToString(intStart, intEnd - intStart));
            }

            return strbSection;
        }
        /// <summary>
        /// Returns a cleaned copy of the section of strbTemplate that begins with strBeginToken and ends with strEndToken
        /// </summary>
        public static StringBuilder GetAndCleanSection(StringBuilder strbTemplate, string strBeginToken, string strEndToken)
        {
            StringBuilder strbSection = new StringBuilder("");
            strbSection = GetSection(strbTemplate, strBeginToken, strEndToken);
            strbSection = CleanSection(strbSection, strBeginToken, strEndToken);
            return strbSection;
        }
        /// <summary>
        /// Returns strOriginal with strVariable replaced by strValue
        /// </summary>
        public static string ReplaceVariable(string strOriginal, string strVariable, string strValue)
        {
            return strOriginal.Replace(strVariable, strValue);
        }
        public static StringBuilder ReplaceVariable(StringBuilder strbOriginal, string strVariable, string strValue)
        {
            return strbOriginal.Replace(strVariable, strValue);
        }
        /// <summary>
        /// Returns strSection without strBeginToken and strEndToken
        /// </summary>
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionClean method in ITS.Common.TemplateHelper
        /// Note: the new API only supports the <!--[section]-->, <!--[/section]--> convention, 
        /// so templates using any other section style will need to be converted to use 
        /// the new convention before calling this API.
        /// </remarks>        
        public static string CleanSection(string strSection, string strBeginToken, string strEndToken)
        {
            string strClean;
            strClean = strSection;
            strClean = strClean.Replace(strBeginToken, "");
            strClean = strClean.Replace(strEndToken, "");
            return strClean;
        }
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionClean method in ITS.Common.TemplateHelper
        /// </remarks>        
		public static StringBuilder CleanSection(StringBuilder strbSection, string strBeginToken, string strEndToken)
        {
            strbSection = strbSection.Replace(strBeginToken, "");
            strbSection = strbSection.Replace(strEndToken, "");
            return strbSection;
        }
        /// <summary>
        /// Returns strTemplate with the section that begins with strBeginToken and ends with strEndToken replaced by strSection
        /// </summary>
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionReplace method in ITS.Common.TemplateHelper
        /// Note: the new API only supports the <!--[section]-->, <!--[/section]--> convention, 
        /// so templates using any other section style will need to be converted to use 
        /// the new convention before calling this API.
        /// </remarks>        
        public static StringBuilder ReplaceSection(StringBuilder strbTemplate, string strBeginToken, string strEndToken, StringBuilder strbSection)
        {
            StringBuilder strbOld;

            strbOld = GetSection(strbTemplate, strBeginToken, strEndToken);
            if (strbOld.Length != 0)
                return strbTemplate.Replace(strbOld.ToString(), strbSection.ToString());

            return strbTemplate;
        }
        /// <summary>
        /// Returns strTemplate with the section that begins with strBeginToken and ends with strEndToken replaced by strSection
        /// </summary>
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionReplace method in ITS.Common.TemplateHelper
        /// Note: the new API only supports the <!--[section]-->, <!--[/section]--> convention, 
        /// so templates using any other section style will need to be converted to use 
        /// the new convention before calling this API.
        /// </remarks>        
        public static string ReplaceSection(string strTemplate, string strBeginToken, string strEndToken, string strSection)
        {
            string strOld;
            strOld = GetSection(strTemplate, strBeginToken, strEndToken);
            if (strOld.Length == 0)
                return strTemplate;
            else
                return strTemplate.Replace(strOld, strSection);
        }
        /// <summary>
        /// Returns strTemplate with the section that begins with strBeginToken and ends with strEndToken replaced by strSection
        /// </summary>
        /// <remarks>
        /// This method is marked as obsolete. Please use SectionReplace method in ITS.Common.TemplateHelper
        /// Note: the new API only supports the <!--[section]-->, <!--[/section]--> convention, 
        /// so templates using any other section style will need to be converted to use 
        /// the new convention before calling this API.
        /// </remarks>        
        public static StringBuilder ReplaceSection(StringBuilder strbTemplate, string strBeginToken, string strEndToken, string strSection)
        {
            StringBuilder strbOld;

            strbOld = GetSection(strbTemplate, strBeginToken, strEndToken);
            if (strbOld.Length != 0)
                return strbTemplate.Replace(strbOld.ToString(), strSection);

            return strbTemplate;
        }
    }
}
