// MbUnit Library 
// 
// Copyright (c) 2004 Jonathan de Halleux
//
// This software is provided 'as-is', without any express or implied warranty. 
// 
// In no event will the authors be held liable for any damages arising from 
// the use of this software.
// Permission is granted to anyone to use this software for any purpose, 
// including commercial applications, and to alter it and redistribute it 
// freely, subject to the following restrictions:
//
//		1. The origin of this software must not be misrepresented; 
//		you must not claim that you wrote the original software. 
//		If you use this software in a product, an acknowledgment in the product 
//		documentation would be appreciated but is not required.
//
//		2. Altered source versions must be plainly marked as such, and must 
//		not be misrepresented as being the original software.
//
//		3. This notice may not be removed or altered from any source 
//		distribution.
//		
//		QuickGraph Library HomePage: http://www.mbunit.org
//		Author: Jonathan de Halleux

//	Original XmlUnit license
/*
******************************************************************
Copyright (c) 2001, Jeff Martin, Tim Bacon
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions
are met:

    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above
      copyright notice, this list of conditions and the following
      disclaimer in the documentation and/or other materials provided
      with the distribution.
    * Neither the name of the xmlunit.sourceforge.net nor the names
      of its contributors may be used to endorse or promote products
      derived from this software without specific prior written
      permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
"AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS
FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE
COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT,
INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN
ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
POSSIBILITY OF SUCH DAMAGE.

******************************************************************
*/



namespace MbUnit.Framework.Xml 
{
    using System.IO;
    using System.Xml;
    
    public class XmlInput {
        private delegate XmlReader XmlInputTranslator(object originalInput, string baseURI);
        private readonly string _baseURI;
        private readonly object _originalInput;
        private readonly XmlInputTranslator _translateInput;
    	private static readonly string CURRENT_FOLDER = ".";
    	
    	private XmlInput(string baseURI, object someXml, XmlInputTranslator translator) {
    		_baseURI = baseURI;
    	     _originalInput = someXml;
            _translateInput = translator;
        }
        
        public XmlInput(string someXml, string baseURI) :
        	this(baseURI, someXml, new XmlInputTranslator(TranslateString)) {
        }
        
        public XmlInput(string someXml) :
        	this(someXml, CURRENT_FOLDER) {
        }
        
        private static XmlReader TranslateString(object originalInput, string baseURI) {
            return new XmlTextReader(baseURI, new StringReader((string) originalInput));
        }
        
        public XmlInput(Stream someXml, string baseURI) :
        	this(baseURI, someXml, new XmlInputTranslator(TranslateStream)) {
        }
        
        public XmlInput(Stream someXml) :
        	this(someXml, CURRENT_FOLDER) {
        }
                
        private static XmlReader TranslateStream(object originalInput, string baseURI) {
            return new XmlTextReader(baseURI, new StreamReader((Stream) originalInput));
        }
        
        public XmlInput(TextReader someXml, string baseURI) :
        	this(baseURI, someXml, new XmlInputTranslator(TranslateReader)) {
        }
        
        public XmlInput(TextReader someXml) :
        	this(someXml, CURRENT_FOLDER) {
        }
                
        private static XmlReader TranslateReader(object originalInput, string baseURI) {
            return new XmlTextReader(baseURI, (TextReader) originalInput);
        }
        
        public XmlInput(XmlReader someXml) :
        	this(null, someXml, new XmlInputTranslator(NullTranslator)) {
        }
                
        private static XmlReader NullTranslator(object originalInput, string baseURI) {
            return (XmlReader) originalInput;
        }
        
        public XmlReader CreateXmlReader() {
            return _translateInput(_originalInput, _baseURI);
        }
                
        public override bool Equals(object other) {
            if (other != null && other is XmlInput) {
                return _originalInput.Equals(((XmlInput)other)._originalInput);
            }
            return false;
        }
        
        public override int GetHashCode() {
            return _originalInput.GetHashCode();
        }
    }
}
