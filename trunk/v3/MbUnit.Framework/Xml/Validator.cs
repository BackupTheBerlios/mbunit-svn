
namespace MbUnit.Framework.Xml 
{
    using System.IO;
    using System.Xml;
    using System.Xml.Schema;

    /// <summary>
    /// <para>
    /// The <b>MbUnit.Framework.Xml</b> contains Xml-specific assertion.
    /// The classes of this namespace are extracted from the XmlUnit project. 
    /// </para>
    /// <code>
    /// /*
    /// ******************************************************************
    /// Copyright (c) 2001, Jeff Martin, Tim Bacon
    /// All rights reserved.
    /// 
    /// Redistribution and use in source and binary forms, with or without
    /// modification, are permitted provided that the following conditions
    /// are met:
    /// 
    /// 	* Redistributions of source code must retain the above copyright
    /// 	  notice, this list of conditions and the following disclaimer.
    /// 	* Redistributions in binary form must reproduce the above
    /// 	  copyright notice, this list of conditions and the following
    /// 	  disclaimer in the documentation and/or other materials provided
    /// 	  with the distribution.
    /// 	* Neither the name of the xmlunit.sourceforge.net nor the names
    /// 	  of its contributors may be used to endorse or promote products
    /// 	  derived from this software without specific prior written
    /// 	  permission.
    /// 
    /// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
    /// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
    /// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
    /// FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
    /// COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
    /// INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
    /// BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
    /// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
    /// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
    /// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
    /// ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
    /// POSSIBILITY OF SUCH DAMAGE.
    /// 
    /// ******************************************************************
    /// */
    /// </code>
    /// </summary>
    public class Validator {
        private bool hasValidated = false;
        private bool isValid = true;
        private string validationMessage;
        private readonly XmlValidatingReader validatingReader;
    	
    	private Validator(XmlReader xmlInputReader) {
            validatingReader = new XmlValidatingReader(xmlInputReader);          
            AddValidationEventHandler(new ValidationEventHandler(ValidationFailed));            
    	}

        public Validator(XmlInput input) :
        	this(input.CreateXmlReader()) {
        }
                                
        public void AddValidationEventHandler(ValidationEventHandler handler) {
            validatingReader.ValidationEventHandler += handler;
        }
        
        public void ValidationFailed(object sender, ValidationEventArgs e) {
            isValid = false;
            validationMessage = e.Message;
        }
        
        private void Validate() {
            if (!hasValidated) {
                hasValidated = true;
                while (validatingReader.Read()) {
                    // only interested in ValidationFailed callbacks
                }
                validatingReader.Close();
            }
        }
        
        public bool IsValid {
            get {
                Validate();
                return isValid;
            }
        }
        
        public string ValidationMessage {
            get {
                Validate();
                return validationMessage;
            }
            
        }
    }
}
