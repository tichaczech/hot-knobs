import React from 'react';
import { Link } from '@tanstack/react-router';
import { useTranslation } from 'react-i18next';
import { Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle } from '@/components/ui/card';
import type { OperatorListItem } from '@/types/models';
import { buttonVariants } from "@/components/ui/button";
import { useAuth } from '@/contexts/auth';

interface OperatorCardProps extends React.ComponentProps<"div"> {
  operator: OperatorListItem;
}

const OperatorCard: React.FC<OperatorCardProps> = ({ operator, ...props }) => {
  const { isAdmin } = useAuth();
  const { t } = useTranslation();

  return (
    <Card {...props}>
      <CardHeader>
        <CardTitle>{operator.name}</CardTitle>
        <CardDescription>{operator.description}</CardDescription>
      </CardHeader>
      <CardContent>
      </CardContent>
      <CardFooter className="flex justify-between">
        {isAdmin ? (
          <Link to='/admin/operators/$id/edit' params={{ id: operator.id }} className={buttonVariants({ variant: "default" })}>
            {t('_common.edit')}
          </Link>
        ) : <div />}
        <Link to='/admin/operators/$id/view' params={{ id: operator.id }} className={buttonVariants({ variant: "outline" })}>
          {t('_common.view')}
        </Link>
      </CardFooter>
    </Card>
  );
};

export default OperatorCard;
