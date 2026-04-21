import {
  Admin,
  ClientMembership,
  Coach,
  Client,
  Equipment,
  GroupTrainingSession,
  Location,
  Manager,
  MembershipType,
  PersonalTrainingSession,
  Room,
} from '../models/types';

// ─── Location ────────────────────────────────────────────────────────────────

export const MOCK_LOCATION: Location = {
  id: 'loc-1',
  name: 'GymOS Downtown',
  address: '14 Svobody Ave, Lviv, 79000',
};

// ─── Users ───────────────────────────────────────────────────────────────────

export const MOCK_MANAGER: Manager = {
  id: 'usr-mgr-1',
  email: 'manager@gymos.com',
  firstName: 'Olena',
  lastName: 'Kovalenko',
  phoneNumber: '+380671234567',
  userType: 'manager',
  locationId: 'loc-1',
};

export const MOCK_ADMIN: Admin = {
  id: 'usr-adm-1',
  email: 'admin@gymos.com',
  firstName: 'Taras',
  lastName: 'Shevchenko',
  phoneNumber: '+380501112233',
  userType: 'admin',
};

export const MOCK_COACHES: Coach[] = [
  {
    id: 'usr-cch-1',
    email: 'ivan.boxing@gymos.com',
    firstName: 'Ivan',
    lastName: 'Petrenko',
    phoneNumber: '+380931112233',
    userType: 'coach',
    specialization: 'Box',
  },
  {
    id: 'usr-cch-2',
    email: 'maria.swim@gymos.com',
    firstName: 'Maria',
    lastName: 'Sydorenko',
    phoneNumber: '+380962223344',
    userType: 'coach',
    specialization: 'Swim',
  },
  {
    id: 'usr-cch-3',
    email: 'dmytro.karate@gymos.com',
    firstName: 'Dmytro',
    lastName: 'Bondarenko',
    phoneNumber: '+380993334455',
    userType: 'coach',
    specialization: 'Karate',
  },
];

export const MOCK_CLIENTS: Client[] = [
  {
    id: 'usr-cli-1',
    email: 'anna.kl@gmail.com',
    firstName: 'Anna',
    lastName: 'Klymenko',
    phoneNumber: '+380671234001',
    userType: 'client',
  },
  {
    id: 'usr-cli-2',
    email: 'bohdan.m@gmail.com',
    firstName: 'Bohdan',
    lastName: 'Marchenko',
    phoneNumber: '+380671234002',
    userType: 'client',
  },
  {
    id: 'usr-cli-3',
    email: 'sofia.h@gmail.com',
    firstName: 'Sofia',
    lastName: 'Hanenko',
    phoneNumber: '+380671234003',
    userType: 'client',
  },
  {
    id: 'usr-cli-4',
    email: 'vlad.o@gmail.com',
    firstName: 'Vladyslav',
    lastName: 'Ostapenko',
    phoneNumber: '+380671234004',
    userType: 'client',
  },
];

// ─── Rooms ───────────────────────────────────────────────────────────────────

export const MOCK_ROOMS: Room[] = [
  { id: 'room-1', locationId: 'loc-1', roomType: 'pool', capacity: 20 },
  { id: 'room-2', locationId: 'loc-1', roomType: 'SPA', capacity: 10 },
  { id: 'room-3', locationId: 'loc-1', roomType: 'hammam', capacity: 8 },
];

// ─── Equipment ───────────────────────────────────────────────────────────────

export const MOCK_EQUIPMENT: Equipment[] = [
  { id: 'eq-1', locationId: 'loc-1', equipmentType: 'dumbbells', quantity: 30 },
  { id: 'eq-2', locationId: 'loc-1', equipmentType: 'barbells', quantity: 10 },
  { id: 'eq-3', locationId: 'loc-1', equipmentType: 'treadmills', quantity: 8 },
];

// ─── Membership Types ────────────────────────────────────────────────────────

export const MOCK_MEMBERSHIP_TYPES: MembershipType[] = [
  {
    id: 'mtype-1',
    name: 'Basic',
    description: 'Access to gym floor and group classes.',
    duration: 30,
    price: 499,
  },
  {
    id: 'mtype-2',
    name: 'Standard',
    description: 'Basic + pool and SPA access.',
    duration: 30,
    price: 799,
  },
  {
    id: 'mtype-3',
    name: 'Premium',
    description: 'All access + 2 personal training sessions.',
    duration: 30,
    price: 1299,
  },
  {
    id: 'mtype-4',
    name: 'Annual Basic',
    description: 'Full year of gym floor access.',
    duration: 365,
    price: 4499,
  },
];

// ─── Client Memberships ──────────────────────────────────────────────────────

export const MOCK_CLIENT_MEMBERSHIPS: ClientMembership[] = [
  {
    id: 'cm-1',
    membershipTypeId: 'mtype-3',
    clientId: 'usr-cli-1',
    startDate: '2025-03-01',
    endDate: '2025-03-31',
    status: 'expired',
  },
  {
    id: 'cm-2',
    membershipTypeId: 'mtype-2',
    clientId: 'usr-cli-1',
    startDate: '2025-04-01',
    endDate: '2025-04-30',
    status: 'active',
  },
  {
    id: 'cm-3',
    membershipTypeId: 'mtype-1',
    clientId: 'usr-cli-2',
    startDate: '2025-04-10',
    endDate: '2025-05-10',
    status: 'active',
  },
  {
    id: 'cm-4',
    membershipTypeId: 'mtype-4',
    clientId: 'usr-cli-3',
    startDate: '2025-01-01',
    endDate: '2025-12-31',
    status: 'active',
  },
];

// ─── Sessions ────────────────────────────────────────────────────────────────

const today = new Date();
const d = (offsetDays: number, h: number, m = 0) => {
  const dt = new Date(today);
  dt.setDate(dt.getDate() + offsetDays);
  dt.setHours(h, m, 0, 0);
  return dt.toISOString();
};

export const MOCK_PERSONAL_SESSIONS: PersonalTrainingSession[] = [
  {
    id: 'ps-1',
    clientId: 'usr-cli-1',
    coachId: 'usr-cch-1',
    roomId: 'room-2',
    name: 'Boxing Fundamentals',
    startTime: d(0, 9),
    endTime: d(0, 10),
  },
  {
    id: 'ps-2',
    clientId: 'usr-cli-2',
    coachId: 'usr-cch-2',
    roomId: 'room-1',
    name: 'Swim Technique',
    startTime: d(1, 11),
    endTime: d(1, 12),
  },
  {
    id: 'ps-3',
    clientId: 'usr-cli-3',
    coachId: 'usr-cch-1',
    roomId: 'room-2',
    name: 'Advanced Boxing',
    startTime: d(2, 14),
    endTime: d(2, 15),
  },
  {
    id: 'ps-4',
    clientId: 'usr-cli-4',
    coachId: 'usr-cch-3',
    roomId: 'room-3',
    name: 'Karate Intro',
    startTime: d(3, 10),
    endTime: d(3, 11),
  },
];

export const MOCK_GROUP_SESSIONS: GroupTrainingSession[] = [
  {
    id: 'gs-1',
    coachId: 'usr-cch-2',
    roomId: 'room-1',
    name: 'Morning Swim',
    description: 'Open-water technique and endurance.',
    capacity: 15,
    startTime: d(0, 8),
    endTime: d(0, 9),
    enrolledClientIds: ['usr-cli-1', 'usr-cli-3'],
  },
  {
    id: 'gs-2',
    coachId: 'usr-cch-1',
    roomId: 'room-2',
    name: 'Boxing Bootcamp',
    description: 'High-intensity boxing circuit.',
    capacity: 12,
    startTime: d(0, 18),
    endTime: d(0, 19),
    enrolledClientIds: ['usr-cli-2'],
  },
  {
    id: 'gs-3',
    coachId: 'usr-cch-3',
    roomId: 'room-3',
    name: 'Karate Kata',
    description: 'Traditional kata practice for all levels.',
    capacity: 10,
    startTime: d(1, 10),
    endTime: d(1, 11),
    enrolledClientIds: ['usr-cli-1', 'usr-cli-2', 'usr-cli-4'],
  },
  {
    id: 'gs-4',
    coachId: 'usr-cch-2',
    roomId: 'room-1',
    name: 'Aqua Aerobics',
    description: 'Low-impact water fitness.',
    capacity: 20,
    startTime: d(2, 9),
    endTime: d(2, 10),
    enrolledClientIds: ['usr-cli-3', 'usr-cli-4'],
  },
  {
    id: 'gs-5',
    coachId: 'usr-cch-1',
    roomId: 'room-2',
    name: 'Friday Sparring',
    description: 'Controlled sparring for intermediate+ students.',
    capacity: 8,
    startTime: d(4, 17),
    endTime: d(4, 18, 30),
    enrolledClientIds: ['usr-cli-1'],
  },
];
