'use client';

import { Inbox, BarChart3, Settings } from 'lucide-react';
import Link from 'next/link';
import { usePathname } from 'next/navigation'; // Hook to get current path

export function Sidebar() {
  const pathname = usePathname();

  const navItems = [
    { href: '/', icon: Inbox, label: 'Inbox' },
    { href: '/analytics', icon: BarChart3, label: 'Analytics' },
    { href: '/settings', icon: Settings, label: 'Settings' },
  ];

  return (
    <aside className="w-64 bg-zinc-950 p-6 border-r border-zinc-800 flex flex-col">
      <div className="text-2xl font-bold mb-10">FeedbackFlow</div>
      <nav className="space-y-4">
        {navItems.map((item) => (
          <Link
            key={item.label}
            href={item.href}
            className={`flex items-center gap-3 text-lg p-2 rounded-md ${
              pathname === item.href
                ? 'bg-zinc-800 text-white'
                : 'text-zinc-400 hover:bg-zinc-800 hover:text-white'
            }`}
          >
            <item.icon size={24} />
            <span>{item.label}</span>
          </Link>
        ))}
      </nav>
    </aside>
  );
}