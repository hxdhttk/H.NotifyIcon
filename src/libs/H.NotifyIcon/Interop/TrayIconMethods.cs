﻿using H.NotifyIcon.Core;

namespace H.NotifyIcon.Interop;

/// <summary>
/// A Interop proxy to for a taskbar icon (NotifyIcon) that sits in the system's
/// taskbar notification area ("system tray").
/// </summary>
[SupportedOSPlatform("windows5.1.2600")]
internal static class TrayIconMethods
{
    private static bool SendMessage(NOTIFY_ICON_MESSAGE command, NOTIFYICONDATAW32 data)
    {
        return PInvoke.Shell_NotifyIcon(command, in data);
    }

    private static bool SendMessage(NOTIFY_ICON_MESSAGE command, NOTIFYICONDATAW64 data)
    {
        return PInvoke.Shell_NotifyIcon(command, in data);
    }

    private static bool SendModifyMessage(NOTIFYICONDATAW32 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_MODIFY, data);
    }

    private static bool SendModifyMessage(NOTIFYICONDATAW64 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_MODIFY, data);
    }

    private static bool SendAddMessage(NOTIFYICONDATAW32 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_ADD, data);
    }

    private static bool SendAddMessage(NOTIFYICONDATAW64 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_ADD, data);
    }

    private static bool SendDeleteMessage(NOTIFYICONDATAW32 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_DELETE, data);
    }

    private static bool SendDeleteMessage(NOTIFYICONDATAW64 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_DELETE, data);
    }

    private static bool SendSetVersionMessage(NOTIFYICONDATAW32 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_SETVERSION, data);
    }

    private static bool SendSetVersionMessage(NOTIFYICONDATAW64 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_SETVERSION, data);
    }

    private static bool SendSetFocusMessage(NOTIFYICONDATAW32 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_SETFOCUS, data);
    }

    private static bool SendSetFocusMessage(NOTIFYICONDATAW64 data)
    {
        return SendMessage(NOTIFY_ICON_MESSAGE.NIM_SETFOCUS, data);
    }

    public static unsafe bool TryCreate(
        Guid id,
        nint handle,
        NOTIFY_ICON_DATA_FLAGS additionalFlags,
        string toolTip,
        uint uCallbackMessage,
        nint iconHandle)
    {
        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags =
                    additionalFlags |
                    NOTIFY_ICON_DATA_FLAGS.NIF_MESSAGE |
                    NOTIFY_ICON_DATA_FLAGS.NIF_ICON |
                    NOTIFY_ICON_DATA_FLAGS.NIF_TIP |
                    NOTIFY_ICON_DATA_FLAGS.NIF_STATE |
                    NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
                hWnd = new HWND(handle),
                guidItem = id,
                uCallbackMessage = uCallbackMessage,
                hIcon = new HICON(iconHandle),
                dwState = (uint)NOTIFY_ICON_STATE.NIS_HIDDEN,
                dwStateMask = (uint)NOTIFY_ICON_STATE.NIS_HIDDEN,
            };
            toolTip.SetTo(&data.szTip._0, data.szTip.Length);

            return SendAddMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags =
                    additionalFlags |
                    NOTIFY_ICON_DATA_FLAGS.NIF_MESSAGE |
                    NOTIFY_ICON_DATA_FLAGS.NIF_ICON |
                    NOTIFY_ICON_DATA_FLAGS.NIF_TIP |
                    NOTIFY_ICON_DATA_FLAGS.NIF_STATE |
                    NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
                hWnd = new HWND(handle),
                guidItem = id,
                uCallbackMessage = uCallbackMessage,
                hIcon = new HICON(iconHandle),
                dwState = (uint)NOTIFY_ICON_STATE.NIS_HIDDEN,
                dwStateMask = (uint)NOTIFY_ICON_STATE.NIS_HIDDEN,
            };
            toolTip.SetTo(&data.szTip._0, data.szTip.Length);

            return SendAddMessage(data);
        }
    }

    public static unsafe bool TryModifyToolTip(
        Guid id,
        string toolTip,
        bool useStandardTooltip)
    {
        var uFlags =
            NOTIFY_ICON_DATA_FLAGS.NIF_TIP |
            NOTIFY_ICON_DATA_FLAGS.NIF_GUID;

        if (useStandardTooltip)
        {
            uFlags |= NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP;
        }

        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = uFlags,
                guidItem = id,
            };
            toolTip.SetTo(&data.szTip._0, data.szTip.Length);

            return SendModifyMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = uFlags,
                guidItem = id,
            };
            toolTip.SetTo(&data.szTip._0, data.szTip.Length);

            return SendModifyMessage(data);
        }
    }

    public static unsafe bool TryModifyIcon(
        Guid id,
        nint iconHandle,
        bool useStandardTooltip)
    {
        var uFlags =
            NOTIFY_ICON_DATA_FLAGS.NIF_ICON |
            NOTIFY_ICON_DATA_FLAGS.NIF_GUID;

        if (useStandardTooltip)
        {
            uFlags |= NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP;
        }

        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = uFlags,
                guidItem = id,
                hIcon = new HICON(iconHandle),
            };

            return SendModifyMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = uFlags,
                guidItem = id,
                hIcon = new HICON(iconHandle),
            };

            return SendModifyMessage(data);
        }
    }

    public static unsafe bool TryModifyState(
        Guid id,
        uint state,
        bool useStandardTooltip)
    {
        var uFlags =
            NOTIFY_ICON_DATA_FLAGS.NIF_STATE |
            NOTIFY_ICON_DATA_FLAGS.NIF_GUID;

        if (useStandardTooltip)
        {
            uFlags |= NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP;
        }

        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = uFlags,
                guidItem = id,
                dwState = state,
                dwStateMask = (uint)NOTIFY_ICON_STATE.NIS_HIDDEN,
            };

            return SendModifyMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = uFlags,
                guidItem = id,
                dwState = state,
                dwStateMask = (uint)NOTIFY_ICON_STATE.NIS_HIDDEN,
            };

            return SendModifyMessage(data);
        }
    }

    public static unsafe bool TryDelete(
        Guid id)
    {
        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
                guidItem = id,
            };

            return SendDeleteMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
                guidItem = id,
            };

            return SendDeleteMessage(data);
        }
    }

    public static unsafe bool TryShowNotification(
        Guid id,
        NOTIFY_ICON_DATA_FLAGS additionalFlags,
        string title,
        string message,
        NOTIFY_ICON_INFOTIP_FLAGS infoFlags,
        nint balloonIconHandle,
        uint timeoutInMilliseconds)
    {
        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = additionalFlags |
                    NOTIFY_ICON_DATA_FLAGS.NIF_INFO |
                    NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
                guidItem = id,
                dwInfoFlags = (uint)infoFlags,
                hBalloonIcon = new HICON(balloonIconHandle),
                Anonymous =
                {
                    uTimeout = timeoutInMilliseconds,
                }
            };
            message.SetTo(&data.szInfo._0, data.szInfo.Length);
            title.SetTo(&data.szInfoTitle._0, data.szInfoTitle.Length);

            return SendModifyMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = additionalFlags |
                    NOTIFY_ICON_DATA_FLAGS.NIF_INFO |
                    NOTIFY_ICON_DATA_FLAGS.NIF_GUID,
                guidItem = id,
                dwInfoFlags = (uint)infoFlags,
                hBalloonIcon = new HICON(balloonIconHandle),
                Anonymous =
                {
                    uTimeout = timeoutInMilliseconds,
                }
            };
            message.SetTo(&data.szInfo._0, data.szInfo.Length);
            title.SetTo(&data.szInfoTitle._0, data.szInfoTitle.Length);

            return SendModifyMessage(data);
        }
    }

    public static unsafe bool TrySetVersion(
        Guid id,
        IconVersion version,
        bool useStandardTooltip)
    {
        var uFlags = NOTIFY_ICON_DATA_FLAGS.NIF_GUID;

        if (useStandardTooltip)
        {
            uFlags |= NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP;
        }

        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = uFlags,
                guidItem = id,
                Anonymous =
                {
                    uVersion = (uint)version,
                }
            };

            return SendSetVersionMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = uFlags,
                guidItem = id,
                Anonymous =
                {
                    uVersion = (uint)version,
                }
            };

            return SendSetVersionMessage(data);
        }
    }

    public static bool TrySetMostRecentVersion(
        Guid id,
        bool useStandardTooltip,
        out IconVersion version)
    {
        version = IconVersion.Vista;
        var status = TrySetVersion(
            id: id,
            version: version,
            useStandardTooltip);
        if (!status)
        {
            version = IconVersion.Win2000;
            status = TrySetVersion(
                id: id,
                version: version,
                useStandardTooltip);
        }
        if (!status)
        {
            version = IconVersion.Win95;
            status = TrySetVersion(
                id: id,
                version: version,
                useStandardTooltip);
        }

        return status;
    }

    public static unsafe bool TrySetFocus(Guid id, bool useStandardTooltip)
    {
        var uFlags = NOTIFY_ICON_DATA_FLAGS.NIF_GUID;

        if (useStandardTooltip)
        {
            uFlags |= NOTIFY_ICON_DATA_FLAGS.NIF_SHOWTIP;
        }

        if (Environment.Is64BitProcess)
        {
            var data = new NOTIFYICONDATAW64
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW64),
                uFlags = uFlags,
                guidItem = id,
            };

            return SendSetFocusMessage(data);
        }
        else
        {
            var data = new NOTIFYICONDATAW32
            {
                cbSize = (uint)sizeof(NOTIFYICONDATAW32),
                uFlags = uFlags,
                guidItem = id,
            };

            return SendSetFocusMessage(data);
        }
    }
}
