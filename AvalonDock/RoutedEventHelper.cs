// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;

// namespace AvalonDock
// {
//     using System;
//     using Avalonia;

//     internal static class RoutedEventHelper
//     {
//         #region RoutedEvent Helper Methods

//         /// <summary>
//         /// A static helper method to raise a routed event on a target Control or ContentElement.
//         /// </summary>
//         /// <param name="target">Control or ContentElement on which to raise the event</param>
//         /// <param name="args">RoutedEventArgs to use when raising the event</param>
//         internal static void RaiseEvent(AvaloniaObject target, RoutedEventArgs args)
//         {
//             if (target is Control)
//             {
//                 (target as Control).RaiseEvent(args);
//             }
//             else if (target is ContentElement)
//             {
//                 (target as ContentElement).RaiseEvent(args);
//             }
//         }

//         /// <summary>
//         /// A static helper method that adds a handler for a routed event 
//         /// to a target Control or ContentElement.
//         /// </summary>
//         /// <param name="element">Control or ContentElement that listens to the event</param>
//         /// <param name="routedEvent">Event that will be handled</param>
//         /// <param name="handler">Event handler to be added</param>
//         internal static void AddHandler(AvaloniaObject element, RoutedEvent routedEvent, Delegate handler)
//         {
//             Control uie = element as Control;
//             if (uie != null)
//             {
//                 uie.AddHandler(routedEvent, handler);
//             }
//             else
//             {
//                 ContentElement ce = element as ContentElement;
//                 if (ce != null)
//                 {
//                     ce.AddHandler(routedEvent, handler);
//                 }
//             }
//         }

//         /// <summary>
//         /// A static helper method that removes a handler for a routed event 
//         /// from a target Control or ContentElement.
//         /// </summary>
//         /// <param name="element">Control or ContentElement that listens to the event</param>
//         /// <param name="routedEvent">Event that will no longer be handled</param>
//         /// <param name="handler">Event handler to be removed</param>
//         internal static void RemoveHandler(AvaloniaObject element, RoutedEvent routedEvent, Delegate handler)
//         {
//             Control uie = element as Control;
//             if (uie != null)
//             {
//                 uie.RemoveHandler(routedEvent, handler);
//             }
//             else
//             {
//                 ContentElement ce = element as ContentElement;
//                 if (ce != null)
//                 {
//                     ce.RemoveHandler(routedEvent, handler);
//                 }
//             }
//         }

//         #endregion
//     }
// }
