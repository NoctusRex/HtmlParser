using HtmlParser.Attributes;
using HtmlParser.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HtmlParser.Models
{
    /// <summary>
    /// Represents an html element like <span></span> or <div></div>
    /// </summary>
    public class HtmlElement : HtmlObject
    {
        /// <summary>
        /// The html element contained in this html element
        /// </summary>
        public IEnumerable<HtmlElement> Elements { get; set; }
        /// <summary>
        /// The html Attributes decorating this html element
        /// </summary>
        public IEnumerable<HtmlAttribute> Attributes { get; set; }
        /// <summary>
        /// The content of the this element eg. text in a span elment
        /// </summary>
        public object Content { get; set; }

        /// <summary>
        /// Returns the first found element to the element id. Throws an exception if the element was not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlElement GetFirstElement(string id)
        {
            if (Elements is null) throw new InvalidOperationException("The source sequence is empty.");

            return Elements.First(x => x.Id.SameText(id));
        }

        public HtmlElement GetFirstElement(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds)
                element = element.GetFirstElement(id);

            return element;
        }

        /// <summary>
        /// Returns the first found element to the element id. Returns null if the element was not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlElement GetFirstOrDefaultElement(string id) => Elements?.FirstOrDefault(x => x.Id.SameText(id));

        public HtmlElement GetFirstOrDefaultElement(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds)
                element = element?.GetFirstOrDefaultElement(id);

            return element;
        }

        /// <summary>
        /// Returns the single found element to the element id. Throws an exception if the element was not found or it is contained more then one time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlElement GetSingleElement(string id) => Elements?.SingleOrDefault(x => x.Id.SameText(id));

        public HtmlElement GetSingleElement(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds)
                element = element.GetSingleElement(id);

            return element;
        }

        /// <summary>
        /// Returns the single found element to the element id. Returns null if the element was not found and throws an exception if it is contained more then one time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlElement GetSingleOrDefaultElement(string id)
        {
            if (Elements is null) throw new InvalidOperationException("The source sequence is empty.");

            return Elements.Single(x => x.Id.SameText(id));
        }

        public HtmlElement GetSingleOrDefaultElement(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds)
                element = element?.GetSingleOrDefaultElement(id);

            return element;
        }

        /// <summary>
        /// Returns all elements to the element id. Returns null if no elements were found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<HtmlElement> GetElements(string id) => Elements?.Where(x => x.Id.SameText(id));

        public IEnumerable<HtmlElement> GetElements(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds.SkipLast(1))
                element = element.GetSingleElement(id);

            return element.GetElements(parentIds.Last());
        }

        /// <summary>
        /// Returns the first found Attribute to the Attribute id. Throws an exception if the Attribute was not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlAttribute GetFirstAttribute(string id)
        {
            if (Attributes is null) throw new InvalidOperationException("The source sequence is empty.");

            return Attributes.First(x => x.Id.SameText(id));
        }

        public HtmlAttribute GetFirstAttribute(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds.SkipLast(1))
                element = element.GetFirstElement(id);

            return element.GetFirstAttribute(parentIds.Last());
        }

        /// <summary>
        /// Returns the first found Attribute to the Attribute id. Returns null if the Attribute was not found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlAttribute GetFirstOrDefaultAttribute(string id) => Attributes?.FirstOrDefault(x => x.Id.SameText(id));

        public HtmlAttribute GetFirstOrDefaultAttribute(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds.SkipLast(1))
                element = element.GetFirstElement(id);

            return element.GetFirstOrDefaultAttribute(parentIds.Last());
        }

        /// <summary>
        /// Returns the single found Attribute to the Attribute id. Throws an exception if the Attribute was not found or it is contained more then one time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlAttribute GetSingleAttribute(string id) => Attributes?.SingleOrDefault(x => x.Id.SameText(id));

        public HtmlAttribute GetSingleAttribute(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds.SkipLast(1))
                element = element.GetFirstElement(id);

            return element.GetSingleAttribute(parentIds.Last());
        }

        /// <summary>
        /// Returns the single found Attribute to the Attribute id. Returns null if the Attribute was not found and throws an exception if it is contained more then one time
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HtmlAttribute GetSingleOrDefaultAttribute(string id)
        {
            if (Attributes is null) throw new InvalidOperationException("The source sequence is empty.");

            return Attributes.Single(x => x.Id.SameText(id));
        }

        public HtmlAttribute GetSingleOrDefaultAttribute(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds.SkipLast(1))
                element = element.GetFirstElement(id);

            return element.GetSingleOrDefaultAttribute(parentIds.Last());
        }

        /// <summary>
        /// Returns all Attributes to the Attribute id. Returns null if no Attributes were found
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<HtmlAttribute> GetAttributes(string id) => Attributes?.Where(x => x.Id.SameText(id));

        public IEnumerable<HtmlAttribute> GetAttributes(params string[] parentIds)
        {
            HtmlElement element = this;

            foreach (string id in parentIds.SkipLast(1))
                element = element.GetFirstElement(id);

            return element.GetAttributes(parentIds.Last());
        }

        /// <summary>
        /// Tries to fill an object of type T based on the property names or attributes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FillObject<T>()
        {
            T result = (T)Activator.CreateInstance(typeof(T));

            foreach (PropertyInfo property in result.GetType().GetProperties())
            {
                Attributes.HtmlAttribute attribute = property.GetCustomAttribute<Attributes.HtmlAttribute>();

                if (attribute is null)
                {

                    string id = property.Name;
                    if (id.SameText("Content"))
                    {
                        property.SetValue(result, Convert.ChangeType(Content, property.PropertyType));
                        continue;
                    }

                    SetValue(property, result, GetFirstOrDefaultAttribute(id));
                }
                else
                {
                    if (attribute is AttributeAttribute)
                    {
                        SetValue(property, result, GetFirstOrDefaultAttribute(attribute.Id));
                    }
                    else if (attribute is ElementAttribute)
                    {

                    }
                    else if (attribute is IgnoreAttribute)
                        continue;

                }

            }

            return result;
        }

        private void SetValue(PropertyInfo property, object @object, HtmlAttribute value)
        {
            if (value != null)
                property.SetValue(@object, Convert.ChangeType(value.Value, property.PropertyType));
        }
    }
}
