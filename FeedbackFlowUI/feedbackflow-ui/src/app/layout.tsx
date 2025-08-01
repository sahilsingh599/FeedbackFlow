import type { Metadata } from 'next';
import { Inter } from 'next/font/google';
import './globals.css';
import { Sidebar } from '@/components/Sidebar';

const inter = Inter({ subsets: ['latin'] });

export const metadata: Metadata = {
    title: 'FeedbackFlow',
    description: 'Unified Customer Feedback Analysis Hub',
};

export default function RootLayout({
    children,
}: {
    children: React.ReactNode;
}) {
    return (
        <html lang="en" className="dark">
            <body className={inter.className}>
                <div className="flex min-h-screen">
                    <Sidebar />
                    <main className="flex-1 p-8 bg-zinc-900 text-white">
                        {children}
                    </main>
                </div>
            </body>
        </html>
    );
}