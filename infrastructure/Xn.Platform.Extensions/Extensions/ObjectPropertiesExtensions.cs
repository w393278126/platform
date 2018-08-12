using System;
using System.Data;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Xn.Platform.Core.Extensions
{
    public static class ObjectPropertiesExtensions
    {

        /// <summary>
        /// ��������-���ำֵ������
        /// </summary>
        /// <typeparam name="TParent"></typeparam>
        /// <typeparam name="TChild"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static void AutoCopy<TParent, TChild>(TParent parent, TChild child) where TChild : TParent, new()
        {
            var ParentType = typeof(TParent);
            var Properties = ParentType.GetProperties();
            foreach (var Propertie in Properties)
            {
                //ѭ����������
                if (Propertie.CanRead && Propertie.CanWrite)
                {
                    //�������Կ���
                    Propertie.SetValue(child, Propertie.GetValue(parent, null), null);
                }
            }
        }

        /// <summary>
        /// ����ֵת�� �����������ͬ�ֶ�ֵת��
        /// </summary>
        /// <typeparam name="SetObject"></typeparam>
        /// <typeparam name="GetObject"></typeparam>
        /// <param name="getObject"></param>
        /// <returns></returns>
        public static SetObject Mapper<SetObject, GetObject>(GetObject getObject)
        {
            SetObject setObject = Activator.CreateInstance<SetObject>();
            try
            {
                var TypeGetObject = getObject.GetType();//�������  
                var TypeSetObject = typeof(SetObject);
                foreach (PropertyInfo getObjectP in TypeGetObject.GetProperties())//������͵������ֶ�  
                {
                    foreach (PropertyInfo setObjectP in TypeSetObject.GetProperties())
                    {
                        if (setObjectP.Name == getObjectP.Name)//�ж��������Ƿ���ͬ  
                        {
                            setObjectP.SetValue(setObject, getObjectP.GetValue(getObject, null), null);//���s�������Ե�ֵ���Ƹ�d���������  
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return setObject;
        }
    }
}