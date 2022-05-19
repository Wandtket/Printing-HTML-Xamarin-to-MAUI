package crc64ab4b7e8e890d35ff;


public class DevicePrintService_AndroidPrintClient
	extends android.webkit.WebViewClient
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onPageFinished:(Landroid/webkit/WebView;Ljava/lang/String;)V:GetOnPageFinished_Landroid_webkit_WebView_Ljava_lang_String_Handler\n" +
			"";
		mono.android.Runtime.register ("Printing_MAUI.Services.DevicePrintService+AndroidPrintClient, Printing MAUI", DevicePrintService_AndroidPrintClient.class, __md_methods);
	}


	public DevicePrintService_AndroidPrintClient ()
	{
		super ();
		if (getClass () == DevicePrintService_AndroidPrintClient.class)
			mono.android.TypeManager.Activate ("Printing_MAUI.Services.DevicePrintService+AndroidPrintClient, Printing MAUI", "", this, new java.lang.Object[] {  });
	}


	public void onPageFinished (android.webkit.WebView p0, java.lang.String p1)
	{
		n_onPageFinished (p0, p1);
	}

	private native void n_onPageFinished (android.webkit.WebView p0, java.lang.String p1);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
