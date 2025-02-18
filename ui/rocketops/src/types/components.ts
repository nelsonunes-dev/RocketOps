import { ReactNode } from 'react';
import { User, Mission, Launch, LaunchStatus, MissionStatus } from './index';

// Layout components
export interface LayoutProps {
  children: ReactNode;
}

export interface SidebarProps {
  isOpen: boolean;
  onClose: () => void;
}

export interface NavItemProps {
  href: string;
  label: string;
  icon: ReactNode;
  isActive?: boolean;
  badgeCount?: number;
}

export interface BreadcrumbItem {
  label: string;
  href?: string;
}

export interface BreadcrumbsProps {
  items: BreadcrumbItem[];
}

export interface PageHeaderProps {
  title: string;
  description?: string;
  actions?: ReactNode;
  breadcrumbs?: BreadcrumbItem[];
}

// UI components
export interface TabItem {
  key: string;
  label: string;
  content: ReactNode;
  disabled?: boolean;
}

export interface TabsProps {
  items: TabItem[];
  defaultActiveKey?: string;
  onChange?: (key: string) => void;
}

export interface ModalProps {
  isOpen: boolean;
  onClose: () => void;
  title: string;
  children: ReactNode;
  footer?: ReactNode;
  size?: 'sm' | 'md' | 'lg' | 'xl' | 'full';
  closeOnOverlayClick?: boolean;
}

export interface AlertProps {
  type: 'info' | 'success' | 'warning' | 'error';
  title?: string;
  message: string | ReactNode;
  onClose?: () => void;
  closable?: boolean;
}

export interface TooltipProps {
  content: string | ReactNode;
  children: ReactNode;
  position?: 'top' | 'right' | 'bottom' | 'left';
  delay?: number;
}

export interface BadgeProps {
  count: number | string;
  variant?: 'primary' | 'secondary' | 'success' | 'warning' | 'danger' | 'info';
  size?: 'sm' | 'md' | 'lg';
  max?: number;
  dot?: boolean;
  className?: string;
}

// Data display components
export interface StatusBadgeProps {
  status: LaunchStatus | MissionStatus;
  size?: 'sm' | 'md' | 'lg';
}

export interface AvatarProps {
  user: Pick<User, 'firstName' | 'lastName' | 'avatar'>;
  size?: 'xs' | 'sm' | 'md' | 'lg' | 'xl';
  showName?: boolean;
  className?: string;
}

export interface TeamAvatarGroupProps {
  users: Pick<User, 'firstName' | 'lastName' | 'avatar'>[];
  max?: number;
  size?: 'xs' | 'sm' | 'md' | 'lg';
}

export interface CountdownProps {
  targetDate: Date | string;
  onComplete?: () => void;
  size?: 'sm' | 'md' | 'lg';
  variant?: 'default' | 'compact' | 'detailed';
}

// Mission & Launch components
export interface MissionCardProps {
  mission: Mission;
  onClick?: (mission: Mission) => void;
  variant?: 'default' | 'compact' | 'detailed';
}

export interface LaunchCardProps {
  launch: Launch;
  onClick?: (launch: Launch) => void;
  variant?: 'default' | 'compact' | 'detailed';
}

export interface LaunchTimelineProps {
  launches: Launch[];
  highlightCurrent?: boolean;
}

export interface MissionProgressProps {
  mission: Mission;
  showDetails?: boolean;
}

// Form components
export interface SelectOption {
  value: string | number | boolean;
  label: string;
  disabled?: boolean;
  description?: string;
  icon?: ReactNode;
}

export interface SelectProps {
  options: SelectOption[];
  value?: string | number | boolean;
  onChange: (value: string | number | boolean) => void;
  placeholder?: string;
  disabled?: boolean;
  loading?: boolean;
  clearable?: boolean;
  searchable?: boolean;
  multiple?: boolean;
  error?: string;
  label?: string;
  required?: boolean;
  className?: string;
}

export interface DatePickerProps {
  value?: Date;
  onChange: (date: Date | null) => void;
  minDate?: Date;
  maxDate?: Date;
  placeholder?: string;
  disabled?: boolean;
  format?: string;
  showTimeSelect?: boolean;
  error?: string;
  label?: string;
  required?: boolean;
  className?: string;
}

// Chart components
export interface ChartProps {
  data: unknown[];
  height?: number;
  width?: string;
  title?: string;
  subtitle?: string;
  loading?: boolean;
  empty?: boolean;
  emptyMessage?: string;
}

export interface LineChartProps extends ChartProps {
  xKey: string;
  yKeys: {key: string; name: string; color?: string}[];
  showLegend?: boolean;
  showGrid?: boolean;
  showTooltip?: boolean;
  dateFormat?: string;
}

export interface BarChartProps extends ChartProps {
  xKey: string;
  yKeys: {key: string; name: string; color?: string}[];
  stacked?: boolean;
  layout?: 'vertical' | 'horizontal';
  showLegend?: boolean;
  showGrid?: boolean;
  showTooltip?: boolean;
}

export interface PieChartProps extends ChartProps {
  nameKey: string;
  valueKey: string;
  showLegend?: boolean;
  showTooltip?: boolean;
  donut?: boolean;
  innerRadius?: number;
  outerRadius?: number;
}
