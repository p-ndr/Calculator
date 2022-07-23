using System;
using System.Collections;
using System.Collections.Generic;

namespace Calculator
{
	
	/*
	 * This class implements a simple queue for performing Shunting Yard algorithm, using
	 * the predefined LinkedList class of C#. It is designed to store any input.
	 * 
	 * Methods supported:
	 * - Push
	 * - Pop
	 * - Peek
	 * - Length
	 */
	public class CustomQueue<T> : IEnumerable<T>
	{
		private LinkedList<T> queueStorage;
		public CustomQueue()
		{
			queueStorage = new LinkedList<T>();
		}

		/*
		 * This method adds an element to the queue. Note that, in both queue and stack, element
		 * is added to the end of the line, the difference is only in the popping order (and of course
		 * a few other things).
		 * 
		 * Params: <T> e
		 * Returns: none.
		 */
		public void push(T e)
		{
			queueStorage.AddLast(e);
		}

		/*
		 * This method retrieves the first element of the queue, thereby removing it.
		 * (And ensuring FIFO ordering).
		 * 
		 * Params: none;
		 * Returns: <T>
		 */
		public T pop()
		{
			T t = queueStorage.First.Value;
			queueStorage.RemoveFirst();
			return t;
		}

		/*
		 * This method retrieves the first element of the queue without removing it.
		 * 
		 * Params: none
		 * Returns: <T>
		 */
		public T peek()
		{
			return queueStorage.First.Value;
		}

		/*
		 * This method returns the length of the queue.
		 * 
		 * Params: none
		 * Returns: int
		 */
		public int length()
		{
			return queueStorage.Count;
		}

        public IEnumerator<T> GetEnumerator()
        {
            return ((IEnumerable<T>)queueStorage).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)queueStorage).GetEnumerator();
        }
    }
}
