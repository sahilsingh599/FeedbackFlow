import { FeedbackItem } from '@/types';

interface FeedbackTableProps {
  items: FeedbackItem[];
}

export function FeedbackTable({ items }: FeedbackTableProps) {
  if (items.length === 0) {
    return <p className="text-center text-zinc-500 mt-10">No feedback items found. Is your backend running?</p>;
  }

  return (
    <div className="overflow-x-auto rounded-lg border border-zinc-800">
      <table className="min-w-full divide-y divide-zinc-800">
        <thead className="bg-zinc-950">
          <tr>
            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-zinc-400 uppercase tracking-wider">Source</th>
            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-zinc-400 uppercase tracking-wider">Author</th>
            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-zinc-400 uppercase tracking-wider">Feedback</th>
            <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-zinc-400 uppercase tracking-wider">Received</th>
          </tr>
        </thead>
        <tbody className="bg-zinc-900 divide-y divide-zinc-800">
          {items.map((item) => (
            <tr key={item.id}>
              <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">{item.source}</td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-zinc-300">{item.author}</td>
              <td className="px-6 py-4 text-sm text-zinc-300 max-w-md truncate">{item.content}</td>
              <td className="px-6 py-4 whitespace-nowrap text-sm text-zinc-400">{new Date(item.receivedAt).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}