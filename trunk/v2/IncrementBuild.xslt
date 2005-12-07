<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://schemas.microsoft.com/developer/msbuild/2003" version="1.0">

	<xsl:output method="xml" encoding="UTF-8" />
	
	<xsl:variable name="VersionMajor" />
	<xsl:variable name="VersionMinor" />
	<xsl:variable name="VersionBuild" />
	<xsl:variable name="VersionRevision" />

	<xsl:template match="/">
<Project xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
	<PropertyGroup>
		<VersionMajor><xsl:value-of select="$VersionMajor" /></VersionMajor>
		<VersionMinor><xsl:value-of select="$VersionMinor" /></VersionMinor>
		<VersionBuild><xsl:value-of select="$VersionBuild + 1" /></VersionBuild>
		<VersionRevision><xsl:value-of select="$VersionRevision" /></VersionRevision>
	</PropertyGroup>
</Project>
	</xsl:template>
	
</xsl:stylesheet>
