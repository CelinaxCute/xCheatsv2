﻿using System;

namespace AltoHttp
{
    /// <summary>
    /// Queue element completed event arguments
    /// </summary>
    public class QueueElementCompletedEventArgs : EventArgs
    {
        private int _index;
        private QueueElement _element;
        /// <summary>
        /// Contains QueueElementCompleted event args
        /// </summary>
        /// <param name="index"></param>
        /// <param name="element"></param>
        public QueueElementCompletedEventArgs(int index, QueueElement element)
        {
            _index = index;
            _element = element;
        }
        /// <summary>
        /// The index of the completed element
        /// </summary>
        public int Index { get { return _index; } }

        /// <summary>
        /// The index of the completed element
        /// </summary>
        public QueueElement Element { get { return _element; } }
    }
}
