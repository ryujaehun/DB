
// WP_lab07.h : main header file for the WP_lab07 application
//
#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"       // main symbols


// CWP_lab07App:
// See WP_lab07.cpp for the implementation of this class
//

class CWP_lab07App : public CWinAppEx
{
public:
	CWP_lab07App();


// Overrides
public:
	virtual BOOL InitInstance();
	virtual int ExitInstance();

// Implementation

public:
	BOOL  m_bHiColorIcons;

	virtual void PreLoadState();
	virtual void LoadCustomState();
	virtual void SaveCustomState();

	afx_msg void OnAppAbout();
	DECLARE_MESSAGE_MAP()
};

extern CWP_lab07App theApp;
