import { Inbox, BarChart3, Settings } from 'lucide-react';

export function Sidebar() {
    return (
        <aside className="w-64 bg-zinc-950 p-6 border-r border-zinc-800">
            <div className="text-2xl font-bold mb-10">FeedbackFlow</div>
            <nav className="space-y-4">
                <a href="#" className="flex items-center gap-3 text-lg p-2 bg-zinc-800 rounded-md">
                    <Inbox size={24} />
                    <span>Inbox</span>
                </a>
                <a href="#" className="flex items-center gap-3 text-lg p-2 hover:bg-zinc-800 rounded-md text-zinc-400">
                    <BarChart3 size={24} />
                    <span>Analytics</span>
                </a>
                <a href="#" className="flex items-center gap-3 text-lg p-2 hover:bg-zinc-800 rounded-md text-zinc-400">
                    <Settings size={24} />
                    <span>Settings</span>
                </a>
            </nav>
        </aside>
    );
}